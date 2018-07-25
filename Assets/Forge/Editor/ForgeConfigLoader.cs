using UnityEditor;
using UnityEngine;
using Forge.Config;

namespace Forge.Editor
{
    /// <summary>
    ///     Loads and creates the config files for editor use
    /// </summary>
    [InitializeOnLoad]
    public class ForgeConfigLoader : MonoBehaviour
    {
        /// <summary>
        ///     Parse the config for editor usage
        /// </summary>
        static ForgeConfigLoader()
        {
            ConfigParser.InitializeConfigParser();
        }
    }
}
