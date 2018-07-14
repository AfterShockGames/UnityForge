using Forge.Config;

namespace Forge.Settings
{
    /// <summary>
    ///     This class contains all global Forge related config settings.
    /// </summary>
    [ConfigGroup("ForgeConfig", "Forge.cfg")]
    public static class ForgeSettings
    {
        [ConfigItem(4)]
        public static int LogLevel { get; set; }

        [ConfigItem("Info.log")]
        public static string InfoLogFile { get; set; }

        [ConfigItem("Critical.log")]
        public static string CriticalLogFile { get; set; }

        [ConfigItem("Warning.log")]
        public static string WarningLogFile { get; set; }

        [ConfigItem("/Logs")]
        public static string LogDirectory { get; set; }
    }
}
