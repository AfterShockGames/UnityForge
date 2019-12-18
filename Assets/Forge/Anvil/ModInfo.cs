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
        public ModBase ModClass = null;

        /// <summary>
        ///     Value to check if PreInit has been fired
        /// </summary>
        public bool PreInitDone = false;
        
        /// <summary>
        ///     Value to check if Load has been fired
        /// </summary>
        public bool LoadDone = false;
        
        /// <summary>
        ///     Value to check if PostInit has been fired
        /// </summary>
        public bool PostInitDone = false;
    }
}