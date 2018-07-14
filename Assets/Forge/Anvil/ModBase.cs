using System.Collections.Generic;
using UnityEngine;

namespace Forge.Anvil
{
    /// <summary>
    ///     Base class for mods.
    ///     This class is a helper which contains usefull functions for mods.
    /// </summary>
    public class ModBase : MonoBehaviour
    {
        /// <summary>
        ///     Public accessor for the private assetDictionary. 
        ///     This contains all loaded assets for this mod.
        /// </summary>
        public Dictionary<string, GameObject> AssetsDictionary { get { return _assetsDictionary; } }

        /// <summary>
        ///     Private asset dictionary containing all loaded assets.
        /// </summary>
        private readonly Dictionary<string, GameObject> _assetsDictionary = new Dictionary<string, GameObject>();

        /// <summary>
        ///     Gets a gameObject for the assetDictionary inside this mod
        /// </summary>
        /// <param name="gameObjectName">The gameObjects Name</param>
        /// <returns>The requested object</returns>
        public GameObject GetGameObject(string gameObjectName)
        {
            GameObject curGameObject;
            AssetsDictionary.TryGetValue(gameObjectName, out curGameObject);

            return curGameObject;
        }

        /// <summary>
        ///     Adds a gameObject to the dictionary by name
        /// </summary>
        /// <param name="gameObjectName">The GameObject Name</param>
        /// <param name="addGameObject">The gameObject To add</param>
        public void AddGameObject(string gameObjectName, GameObject addGameObject)
        {
            AssetsDictionary.Add(gameObjectName, addGameObject);
        }
    }
}
