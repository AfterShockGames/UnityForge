using System.Reflection;

namespace Forgery.Settings
{
    /// <summary>
    ///     This class contains any Forgery Related settings / Data for internal use
    /// </summary>
    public class InternalData
    {
        #region AnvilModLoading
        public const string ANVIL_REGISTRY = "Anvil";
        public const string PRE_INIT = "PreInit";
        public const string LOAD = "Load";
        public const string POST_INIT = "PostInit";
        #endregion

        #region ForgeryRegistry

        public const string FORGERY_COMPONENT_REGISTRY = "FORGERY_COMPONENT_REGISTRY";
        public const string FORGERY_REGISTRY = "FORGERY_REGISTRY";
        public const string ANVIL_REGISTER = "AnvilRegister";

        #endregion

        #region ForgeryConfig
        public const string GAMES_DIR = "My Games";
        public const string CONFIG_DIR = "Config";
        #endregion

        #region Hammer
        public const string HAMMER = "Hammer";
        #endregion

        #region ForgeryStaticData

        #region Global

        /// <summary>
        ///     The master assembly, this is used to identify the main game from the mods.
        ///     Which helps preventing mods from editing core registries.
        /// </summary>
        public static Assembly MasterAssembly;

        #endregion

        #endregion
    }
}
