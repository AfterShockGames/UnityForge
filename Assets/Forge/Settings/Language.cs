using Forge.Config;

namespace Forge.Settings
{
    [ConfigGroup("Language", "Forge.cfg")]
    public static class Language
    {

        #region LoggingCritical
        [ConfigItem("Mod already registered with ModID:")]
        public static string ForgeCriticalModDoubleID { get; set; }
        [ConfigItem("Mod dependency is missing:")]
        public static string ForgeCriticalModDependencyMissing { get; set; }
        [ConfigItem("Unable to read package bundle:")]
        public static string ForgeCriticalModPackageIssue { get; set; }
        [ConfigItem("Registered type should inherit MonoBehaviour!")]
        public static string ForgeCriticalRegistryInvalidType { get; set; }
        [ConfigItem("Config group class should be static! Provided class name: ")]
        public static string ForgeCriticalInvalidConfigGroupClassType { get; set; }
        [ConfigItem("No event method registered for the called event: ")]
        public static string ForgeCriticalNoEventMethod { get; set; }
        [ConfigItem("Invalid EventType in eventList for eventName: ")]
        public static string ForgeCriticalInvalidEventTypeInList { get; set; }
        [ConfigItem("ArgumentType does not equal EventType")]
        public static string ForgeCriticalArgumentTypeDoesNotEqualEventType { get; set; }
        [ConfigItem("A Registry with this identifier already exists!")]
        public static string ForgeCriticalRegistryAlreadyExists { get; set; }
        #endregion

        #region LoggingInfo

        [ConfigItem("Mod Successfully registered with ModID: ")]
        public static string ForgeInfoModRegistered { get; set; }
        [ConfigItem("Loaded asset: ")]
        public static string ForgeInfoAssetLoaded { get; set; }
        [ConfigItem("Tried to load an item which is not registered item id: ")]
        public static string RegistryItemNotFound { get; set; }
        [ConfigItem("Item already registered, item id: ")]
        public static string RegistryItemRegistered { get; set; }
        [ConfigItem("Tried to delete an item which is not inside the registry item id: ")]
        public static string RegistryDeleteItemNotFound { get; set; }

        #endregion

        #region ModLoading
        [ConfigItem("Mod registrered with modID:")]
        public static string ForgeModRegistered { get; set; }
        [ConfigItem("Mod PreInit fired for mod:")]
        public static string ForgeModPreInitFired { get; set; }
        [ConfigItem("Mod Load fired for mod:")]
        public static string ForgeModLoadFired { get; set; }
        [ConfigItem("Mod PostInit fired for mod:")]
        public static string ForgeModPostInitFired { get; set; }
        #endregion

        #region EditorLanguage
        [ConfigItem("Forge")]
        public static string ForgeTopMenuItemName { get; set; }
        [ConfigItem("Manage Forge Asset Bundles")]
        public static string ForgeAssetBundleManagerMenuItemName { get; set; }
        [ConfigItem("Forge Asset Bundles")]
        public static string ForgeAssetBundleManagerWindowName { get; set; }
        [ConfigItem("Build")]
        public static string ForgeAssetBundleManagerBuildBundle { get; set; }
        #endregion
    }
}
