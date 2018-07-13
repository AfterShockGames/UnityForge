using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Forgery.Errors;
using Forgery.Settings;

namespace Forgery.Config
{
    /// <summary>
    ///     Class which parses all config files based on the given directories.
    /// </summary>
    public class ConfigParser
    {
        /// <summary>
        ///     Dictionary - Holds all config Data
        ///         String - Config file name
        ///         Dictionary
        ///             String - Config Group name
        ///             Dictionary
        ///                 string - Config Item name
        ///                 ConfigParseItem - ConfigParse item holding all function data and information.
        /// </summary>
        private Dictionary<string, Dictionary<string, Dictionary<string, ConfigParseItem>>> _configData = new Dictionary<string, Dictionary<string, Dictionary<string, ConfigParseItem>>>();
        private DirectoryInfo _configDirectory;

        private readonly DirectoryInfo _gameDirectory;


        /// <summary>
        ///     Starts the config Parser
        /// </summary>
        /// <param name="configFolder">The folder to parse from</param>
        public ConfigParser(string configFolder)
        {
            //Gets the specified folder starting from the MyDocuments folder.
            string directory = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), InternalData.GAMES_DIR), configFolder);

            //Create the games directory if it doesn't exist
            if (!Directory.Exists(directory))
            {
                _gameDirectory = Directory.CreateDirectory(directory);
            }
            else
            {
                _gameDirectory = new DirectoryInfo(directory);
            }

            InitializeConfig();
        }

        /// <summary>
        ///     Creates the config file and folders.
        /// </summary>
        private void InitializeConfig()
        {
            string directory = Path.Combine(_gameDirectory.FullName, InternalData.CONFIG_DIR); //Get the Config Dir

            //Create the config dir if it doesn't exist
            if (!Directory.Exists(directory))
            {
                _configDirectory = Directory.CreateDirectory(directory);
            }
            else
            {
                _configDirectory = new DirectoryInfo(directory);
            }

            //Get all config data from all loaded assemblies
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                _configData = _configData.Union(ParseConfig(assembly)).ToDictionary(i => i.Key, i => i.Value);
            }

            Dictionary<string, Dictionary<string, Dictionary<string, ConfigParseItem>>> newConfig = new Dictionary<string, Dictionary<string, Dictionary<string, ConfigParseItem>>>();

            //Write the data to the config files
            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, ConfigParseItem>>> config in _configData)
            {
                string filePath = Path.Combine(directory, config.Key);

                newConfig[config.Key] = WriteConfig(filePath, config.Value);
            }

            _configData = newConfig;

            //Write the config data to the config classes
            foreach(Dictionary<string, Dictionary<string, ConfigParseItem>> config in _configData.Values)
            {
                foreach(Dictionary<string, ConfigParseItem> configItems in config.Values)
                {
                    foreach (ConfigParseItem configItem in configItems.Values)
                    {
                        SaveConfigItem(configItem);
                    }
                }
            }
        }

        /// <summary>
        ///     Saves a config item to the defined classes
        /// </summary>
        /// <param name="configItem">The parse item which holds the info to set the property</param>
        private void SaveConfigItem(ConfigParseItem configItem)
        {
            if (configItem.Property == null)
            {
                return;
            }

            if (configItem.NewValue == null)
            {
                configItem.NewValue = configItem.ConfigDefault;
            }

            //TODO: Replace this with a customizable function to support more types
            object convertedValue = Convert.ChangeType(configItem.NewValue, configItem.Property.PropertyType);

            configItem.Property.SetValue(null, convertedValue, null);
        }

        /// <summary>
        ///     Parses all config classes for file conversion
        /// </summary>
        /// <param name="assembly">The assembly to parse from</param>
        private Dictionary<string, Dictionary<string, Dictionary<string, ConfigParseItem>>> ParseConfig(Assembly assembly = null)
        {
            Dictionary<string, Dictionary<string, Dictionary<string, ConfigParseItem>>> parseDict = new Dictionary<string, Dictionary<string, Dictionary<string, ConfigParseItem>>>();

            foreach (Type type in GenericHelpers.GetTypesWithAttribute(typeof(ConfigGroup), assembly))
            {
                if(!type.IsAbstract && !type.IsSealed)
                {
                    throw new ForgeryCritical(Language.ForgeryCriticalInvalidConfigGroupClassType + type.Name);
                }

                ConfigGroup config = type.GetCustomAttributes(typeof(ConfigGroup), true).FirstOrDefault() as ConfigGroup;

                foreach (PropertyInfo propertyInfo in type.GetProperties())
                {
                    //Turn all config items into ParseItems
                    foreach (ConfigItem configItem in propertyInfo.GetCustomAttributes(typeof(ConfigItem), true))
                    {
                        ConfigParseItem parseItem = new ConfigParseItem
                        {
                            ConfigDefault = configItem.DefaultValue,
                            ConfigName = configItem.Name,
                            Property = propertyInfo,
                        };

                        if(parseItem.ConfigName == null)
                        {
                            parseItem.ConfigName = parseItem.Property.Name;
                        }

                        if (config != null && parseDict.ContainsKey(config.FileName))
                        {
                            if(parseDict[config.FileName].ContainsKey(config.GroupName))
                            {
                                parseDict[config.FileName][config.GroupName].Add(parseItem.ConfigName, parseItem);
                            }
                            else
                            {
                                parseDict[config.FileName].Add(config.GroupName, new Dictionary<string, ConfigParseItem> { { parseItem.ConfigName, parseItem } });
                            }
                            
                        }
                        else
                        {
                            if (config != null)
                            {
                                parseDict.Add(
                                    config.FileName,
                                    new Dictionary<string, Dictionary<string, ConfigParseItem>>
                                    {
                                        {
                                            config.GroupName,
                                            new Dictionary<string, ConfigParseItem> { {parseItem.ConfigName, parseItem} }
                                        }
                                    }
                                );
                            }
                        }
                    }
                }
            }

            return parseDict;
        }

        /// <summary>
        ///     Writes to a config file
        /// </summary>
        /// <param name="filePath">The file path to write to</param>
        /// <param name="configData">
        ///     The config data to write.
        ///     This is a dictionary containing groupnames and configItems
        /// </param>
        /// <returns>
        ///     A modified configData
        /// </returns>
        private Dictionary<string, Dictionary<string, ConfigParseItem>> WriteConfig(string filePath, Dictionary<string, Dictionary<string, ConfigParseItem>> configData)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            configData = configData.Union(ReadConfig(filePath, configData, true)).ToDictionary(i => i.Key, i => i.Value);

            string tmpFile = filePath + ".tmp";

            //We make a backup first to make sure the file isn't lost.
            File.Copy(filePath, tmpFile);

            File.WriteAllText(filePath, string.Empty);

            StreamWriter fileWriter = new StreamWriter(filePath);

            string currentGroup = "";
            bool firstLine = true;

            //Identify and write the config lines
            foreach(KeyValuePair<string, Dictionary<string, ConfigParseItem>> kv in configData)
            {
                if(currentGroup != kv.Key)
                {
                    currentGroup = kv.Key;

                    if(!firstLine)
                    {
                        fileWriter.Write(fileWriter.NewLine);
                    }
                    else
                    {
                        firstLine = false;
                    }

                    fileWriter.WriteLine('[' + kv.Key + ']');
                }

                foreach (ConfigParseItem configItem in kv.Value.Values)
                {
                    if (configItem.NewValue == null)
                    {
                        configItem.NewValue = configItem.ConfigDefault;
                    }

                    fileWriter.WriteLine(configItem.ConfigName + " = " + configItem.NewValue);
                }
            }

            fileWriter.Close();

            File.Delete(tmpFile);

            return configData;
        }

        /// <summary>
        ///     Reads a config file
        /// </summary>
        /// <param name="filePath">The config file path</param>
        /// <param name="configData">
        ///     The config data to match.
        ///     This is a dictionary containing groupnames and configItems
        /// </param>
        /// <param name="readAll">If true this even reads excessive data which do not exist within configGroups</param>
        /// <returns></returns>
        private Dictionary<string, Dictionary<string, ConfigParseItem>> ReadConfig(string filePath, Dictionary<string, Dictionary<string, ConfigParseItem>> configData, bool readAll = false)
        {
            StreamReader config = new StreamReader(filePath);

            ConfigGroup currentGroup = null;

            string line;
            while ((line = config.ReadLine()) != null)
            {
                line = line.Trim();

                //Identifies what type of line we're dealing with. ConfigGroup or ConfigItem
                ConfigLine configLine = IdentifyLine(line);
             
                if(configLine == null)
                {
                    continue;
                }
                   
                if (configLine.GroupName != null)
                {
                    currentGroup = new ConfigGroup(configLine.GroupName, filePath);
                    continue;
                }

                //Check if this is a new value and if it should be saved to the given configData
                if (
                    currentGroup != null && 
                    configLine.ItemName != null && 
                    configLine.ItemValue != null && 
                    configData.ContainsKey(currentGroup.GroupName) &&
                    configData[currentGroup.GroupName].ContainsKey(configLine.ItemName) &&
                    configData[currentGroup.GroupName][configLine.ItemName].ConfigDefault != configLine.ItemValue
                ) {
                    configData[currentGroup.GroupName][configLine.ItemName].NewValue = configLine.ItemValue;
                }
                else if(readAll)
                {
                    if (currentGroup != null && configData.ContainsKey(currentGroup.GroupName))
                    {
                        if (!configData[currentGroup.GroupName].ContainsKey(configLine.ItemName))
                        {
                            configData[currentGroup.GroupName].Add(configLine.ItemName, new ConfigParseItem
                            {
                                ConfigName = configLine.ItemName,
                                NewValue = configLine.ItemValue
                            });
                        }
                    }
                    else
                    {
                        if (currentGroup != null)
                        {
                            configData.Add(currentGroup.GroupName, new Dictionary<string, ConfigParseItem>
                            {
                                {
                                    configLine.ItemName, new ConfigParseItem
                                    {
                                        ConfigName = configLine.ItemName,
                                        NewValue = configLine.ItemValue
                                    }
                                }
                            });
                        }
                    }
                }
            }

            config.Close();

            return configData;
        }

        /// <summary>
        ///     Identifies the config line if it's a group or item
        /// </summary>
        /// <param name="line">The config line</param>
        /// <returns>
        ///     Config line class
        /// </returns>
        private ConfigLine IdentifyLine(string line)
        {
            GroupCollection groups = Regex.Match(line, @"\[([^]]*)\]").Groups;

            if (groups.Count > 1 && groups[1].Value != "") 
            {
                return new ConfigLine
                {
                    GroupName = groups[1].Value
                };
            }

            string[] lineParts = line.Split('=');

            if(lineParts.Length < 2)
            {
                return null;
            }


            return new ConfigLine
            {
                ItemName = lineParts[0].Trim(),
                ItemValue = lineParts[1].Trim()
            };
        }
    }
}
