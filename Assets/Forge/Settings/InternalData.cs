using System.Reflection;
using System.IO;
using System;

namespace Forge.Settings
{
    /// <summary>
    ///     This class contains any Forge Related settings / Data for internal use
    /// </summary>
    public class InternalData
    {
        #region AnvilModLoading
        public const string ANVIL_REGISTRY = "Anvil";
        public const string PRE_INIT = "PreInit";
        public const string LOAD = "Load";
        public const string POST_INIT = "PostInit";
        #endregion

        #region ForgeRegistry

        public const string Forge_COMPONENT_REGISTRY = "Forge_COMPONENT_REGISTRY";
        public const string Forge_REGISTRY = "Forge_REGISTRY";
        public const string ANVIL_REGISTER = "AnvilRegister";

        #endregion

        #region ForgeConfig
        public const string GAMES_DIR = "My Games";
        public const string CONFIG_DIR = "Config";
        public const string ASSET_BUNDLES_DIR = "AssetBundles";
        public static string GamePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), GAMES_DIR);
        #endregion

        #region Hammer
        public const string HAMMER = "Hammer";
        #endregion

        #region ForgeStaticData

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
