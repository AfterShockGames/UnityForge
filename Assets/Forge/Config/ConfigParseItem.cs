using System;
using System.Reflection;

namespace Forge.Config
{
    /// <summary>
    ///     Contains a config item Parse Data.
    /// </summary>
    public class ConfigParseItem
    {
        /// <summary>
        ///     Config Item Name
        /// </summary>
        public string ConfigName;

        /// <summary>
        ///     Config Item Default value
        /// </summary>
        public object ConfigDefault;
        /// <summary>
        ///     ConfigItem new value if found within a config file
        /// </summary>
        public object NewValue;

        /// <summary>
        ///     The configItem Property type
        /// </summary>
        public Type ConfigType;

        /// <summary>
        ///     Property set method reference
        /// </summary>
        public MethodInfo Method;
        /// <summary>
        ///     Config Item property
        /// </summary>
        public PropertyInfo Property;
    }
}
