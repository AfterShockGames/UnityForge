using System;
using System.Collections.Generic;
using System.IO;

namespace Forge
{
    [Obsolete]
    public class ForgeBase
    {
        public static string ModsPath = "Mods";

        private readonly DirectoryInfo _modsDirectory;
        private readonly List<ForgeMod> _ForgeMods = new List<ForgeMod>();
        private readonly ForgeCompiler _ForgeCompiler = new ForgeCompiler();

        /// <summary>
        ///     Used to load arbitrary non core mods from the mod folder
        /// </summary>
        /// <param name="modsPath"></param>
        public ForgeBase(string modsPath = "Mods")
        {
            ModsPath = Path.Combine(Directory.GetCurrentDirectory(), modsPath);

            if (!Directory.Exists(ModsPath))
            {
                _modsDirectory = Directory.CreateDirectory(ModsPath);
            }
            else
            {
                _modsDirectory = new DirectoryInfo(ModsPath);
            }

            foreach (DirectoryInfo modDirectory in _modsDirectory.GetDirectories())
            {
                _ForgeMods.Add(InitializeMod(modDirectory));
            }

            foreach (ForgeMod ForgeMod in _ForgeMods)
            {
                
            }
        }

        public DirectoryInfo GetModsDirectory()
        {
            return _modsDirectory;
        }

        public ForgeMod InitializeMod(DirectoryInfo modDirectory)
        {
            ForgeMod ForgeMod = new ForgeMod(modDirectory, _ForgeCompiler.CompileMod(modDirectory));

            return ForgeMod;
        }
    }
}
