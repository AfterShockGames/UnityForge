using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Forge.Anvil;
using UnityEngine;

namespace Forge
{
    /// <summary>
    /// TODO:: Finish create loading
    /// </summary>
    [Obsolete]
    public class ForgeMod
    {
        private readonly DirectoryInfo _modFolder;
        private readonly Assembly _modAssembly;

        public ForgeMod(DirectoryInfo folder, Assembly assembly)
        {
            _modFolder = folder;
            _modAssembly = assembly;

            string[] files = Directory.GetFiles(_modFolder.FullName, "*", SearchOption.AllDirectories).Where(name => !name.EndsWith(".cs")).ToArray();

            AssetBundle bundle = AssetBundle.LoadFromFile(Path.Combine(folder.FullName, "package"));

            if (bundle == null)
            {
                //TODO:: throw
                Debug.Log("Failed to load assetbundle");
            }

            Debug.Log(_modAssembly);

            foreach (ModInfo modInfo in AnvilRegistry.GetAnvil.GetMods(_modAssembly))
            {
                foreach (string assetNames in bundle.GetAllAssetNames())
                {
                    Debug.Log("Loaded: " + assetNames);

                    GameObject gameObject = bundle.LoadAsset<GameObject>(assetNames);

                    modInfo.ModClass.AddGameObject(gameObject.name, gameObject);
                }
            }
        }
    }
}
