using System;
using System.Reflection;

namespace Forge.Anvil
{
    /// <summary>
    ///     ModInfo class which holds relevant modInformation for modLoading
    /// </summary>
    public class ModInfo
    {
        /// <summary>
        ///     Reference to the modsClass
        /// </summary>
        public object ModClass = null;

        /// <summary>
        ///     The modClasses type
        /// </summary>
        public Type ModClassType = null;

        /// <summary>
        ///     PreInit function
        /// </summary>
        public MethodInfo PreInit = null;
        /// <summary>
        ///     Value to check if PreInit has been fired
        /// </summary>
        public bool PreInitDone = false;

        /// <summary>
        ///     Load function
        /// </summary>
        public MethodInfo Load = null;
        /// <summary>
        ///     Value to check if Load has been fired
        /// </summary>
        public bool LoadDone = false;

        /// <summary>
        ///     PostInit function
        /// </summary>
        public MethodInfo PostInit = null;
        /// <summary>
        ///     Value to check if PostInit has been fired
        /// </summary>
        public bool PostInitDone = false;
    }
}