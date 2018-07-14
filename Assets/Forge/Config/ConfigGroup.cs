using System;

namespace Forge.Config
{
    /// <summary>
    ///     This is a class attribute which will be used during config parsing
    ///     A config group will be converted to a new file if specified and a new ConfigGroup.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ConfigGroup : Attribute
    {
        public readonly string GroupName;
        public readonly string FileName;

        /// <summary>
        ///     Base attribute constructor
        /// </summary>
        /// <param name="groupName">
        ///     The configGroup name, this will be the group holding your configData.
        ///     If for example your group is "Forge" it will result in a new group within the specified config file called [Forge]
        ///     Groups will be bundled if there are duplicated groupNames inside a file.
        /// </param>
        /// <param name="fileName">
        ///     This will be the filename where your group is going to be stored.
        ///     A new file will be created if it already exists
        /// </param>
        public ConfigGroup(string groupName, string fileName)
        {
            GroupName = groupName;
            FileName = fileName;
        }
    }
}
