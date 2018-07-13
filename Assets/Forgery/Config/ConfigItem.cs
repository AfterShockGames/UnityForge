using System;
using System.IO;

namespace Forgery.Config
{
    /// <summary>
    ///     Config Items are a keyValue attribute which will be parsed into Config files and sets a default property value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property)]
    public class ConfigItem : Attribute
    {
        public readonly string Name;
        public readonly object DefaultValue;

        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="defaultValue">
        ///     This will be the default value which will be set in the config if there is no config present.
        /// </param>
        /// <param name="name">
        ///     Config name, if set to zero the propertyName will be used as config name.
        /// </param>
        public ConfigItem(object defaultValue, string name = null)
        {
            Name = name;

            string result = CodeIdentifier((ConfigCode)defaultValue);

            if(result != null)
            {
                DefaultValue = result;
            }
            else
            {
                DefaultValue = defaultValue;
            }
        }

        private string CodeIdentifier(ConfigCode code)
        {
            switch (code)
            {
                case ConfigCode.CurrentDirectory:
                    return Directory.GetCurrentDirectory();
                default:
                    return null;
            }
        }
    }
}
