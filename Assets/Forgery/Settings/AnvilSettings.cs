using Forgery.Config;
using System.IO;

namespace Forgery.Settings
{
    [ConfigGroup("AnvilConfig", "Forgery.cfg")]
    public static class AnvilSettings
    {
        [ConfigItem("/Mods")]
        public static string ModFolder { get; set; }
        [ConfigItem(ConfigCode.CurrentDirectory)]
        public static string GameFolder { get; set; }
    }
}