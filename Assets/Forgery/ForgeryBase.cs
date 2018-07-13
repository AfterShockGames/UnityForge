using System;
using System.Collections.Generic;
using System.IO;

namespace Forgery
{
    [Obsolete]
    public class ForgeryBase
    {
        public static string ModsPath = "Mods";

        private readonly DirectoryInfo _modsDirectory;
        private readonly List<ForgeryMod> _forgeryMods = new List<ForgeryMod>();
        private readonly ForgeryCompiler _forgeryCompiler = new ForgeryCompiler();

        /// <summary>
        ///     Used to load arbitrary non core mods from the mod folder
        /// </summary>
        /// <param name="modsPath"></param>
        public ForgeryBase(string modsPath = "Mods")
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
                _forgeryMods.Add(InitializeMod(modDirectory));
            }

            foreach (ForgeryMod forgeryMod in _forgeryMods)
            {
                
            }
        }

        public DirectoryInfo GetModsDirectory()
        {
            return _modsDirectory;
        }

        public ForgeryMod InitializeMod(DirectoryInfo modDirectory)
        {
            ForgeryMod forgeryMod = new ForgeryMod(modDirectory, _forgeryCompiler.CompileMod(modDirectory));

            return forgeryMod;
        }
    }
}
