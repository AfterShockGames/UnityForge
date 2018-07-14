using Forge.Config;
using System.IO;

namespace Forge.Settings
{
    [ConfigGroup("AnvilConfig", "Forge.cfg")]
    public static class AnvilSettings
    {
        [ConfigItem("/Mods")]
        public static string ModFolder { get; set; }
        [ConfigItem(ConfigCode.CurrentDirectory)]
        public static string GameFolder { get; set; }
    }
}