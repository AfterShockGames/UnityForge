namespace Forge.Config
{

    /// <summary>
    ///     Internal class used during ConfigParsing.
    ///     Represents a line within the configFiles
    /// </summary>
    internal class ConfigLine
    {
        /// <summary>
        ///     The group name which represents a group defined via brackets in a config file.
        /// </summary>
        public string GroupName;
        /// <summary>
        ///     The item name, which represents a single setting key
        /// </summary>
        public string ItemName;
        /// <summary>
        ///     The item value, which represents a single setting value
        /// </summary>
        public object ItemValue;
    }
}
