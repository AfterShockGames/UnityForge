using Forgery.Config;

namespace Forgery.Settings
{
    [ConfigGroup("Language", "Forgery.cfg")]
    public static class Language
    {

        #region LoggingCritical
        [ConfigItem("Mod already registered with ModID:")]
        public static string ForgeryCriticalModDoubleID { get; set; }
        [ConfigItem("Mod dependency is missing:")]
        public static string ForgeryCriticalModDependencyMissing { get; set; }
        [ConfigItem("Unable to read package bundle:")]
        public static string ForgeryCriticalModPackageIssue { get; set; }
        [ConfigItem("Registered type should inherit MonoBehaviour!")]
        public static string ForgeryCriticalRegistryInvalidType { get; set; }
        [ConfigItem("Config group class should be static! Provided class name: ")]
        public static string ForgeryCriticalInvalidConfigGroupClassType { get; set; }
        [ConfigItem("No event method registered for the called event: ")]
        public static string ForgeryCriticalNoEventMethod { get; set; }
        [ConfigItem("Invalid EventType in eventList for eventName: ")]
        public static string ForgeryCriticalInvalidEventTypeInList { get; set; }
        [ConfigItem("ArgumentType does not equal EventType")]
        public static string ForgeryCriticalArgumentTypeDoesNotEqualEventType { get; set; }
        [ConfigItem("A Registry with this identifier already exists!")]
        public static string ForgeryCriticalRegistryAlreadyExists { get; set; }
        #endregion

        #region LoggingInfo

        [ConfigItem("Mod Successfully registered with ModID: ")]
        public static string ForgeryInfoModRegistered { get; set; }
        [ConfigItem("Loaded asset: ")]
        public static string ForgeryInfoAssetLoaded { get; set; }
        [ConfigItem("Tried to load an item which is not registered item id: ")]
        public static string RegistryItemNotFound { get; set; }
        [ConfigItem("Item already registered, item id: ")]
        public static string RegistryItemRegistered { get; set; }
        [ConfigItem("Tried to delete an item which is not inside the registry item id: ")]
        public static string RegistryDeleteItemNotFound { get; set; }

        #endregion

        #region ModLoading
        [ConfigItem("Mod registrered with modID:")]
        public static string ForgeryModRegistered { get; set; }
        [ConfigItem("Mod PreInit fired for mod:")]
        public static string ForgeryModPreInitFired { get; set; }
        [ConfigItem("Mod Load fired for mod:")]
        public static string ForgeryModLoadFired { get; set; }
        [ConfigItem("Mod PostInit fired for mod:")]
        public static string ForgeryModPostInitFired { get; set; }
        #endregion
    }
}
