using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using Forgery.Errors;
using Forgery.Settings;

namespace Forgery.Anvil
{
    /// <summary>
    ///     Forgery Anvil Registry class.
    ///     This class does everything related to mod loading or mod instantiation.
    /// </summary>
    public class AnvilRegistry : MonoBehaviour
    {
        /// <summary>
        ///     Gets or creates the Anvil registry
        /// </summary>
        public static AnvilRegistry GetAnvil
        {
            get
            {
                return _anvilRegistry ?? InstantiateAnvilRegistry;
            }
        }

        /// <summary>
        ///     Creates the Anvil Registry object
        /// </summary>
        public static AnvilRegistry InstantiateAnvilRegistry
        {
            get
            {
                GameObject go = new GameObject(InternalData.ANVIL_REGISTRY);
                _anvilRegistry = go.AddComponent<AnvilRegistry>();

                return _anvilRegistry;
            }
        }

        /// <summary>
        ///     Internal list which holds all loaded ModPackages
        /// </summary>
        internal static Dictionary<string, Tuple<bool, Assembly>> ModPackages = new Dictionary<string, Tuple<bool, Assembly>>();

        /// <summary>
        ///     Holds the AnvilRegistry class for mod registration
        /// </summary>
        private static AnvilRegistry _anvilRegistry;

        /// <summary>
        ///     List containing all registered mods
        /// </summary>
        private readonly Dictionary<string, ModInfo> _registeredMods = new Dictionary<string, ModInfo>();

        /// <summary>
        ///     Gets all mods, this should be fired before gameInitialization.
        /// </summary>
        public void GetMods()
        {
            foreach (Type type in GenericHelpers.GetTypesWithAttribute(typeof(Mod), null))
            {
                ModInfo info = TypeToModInfo(type);

                Logging.Info(Language.ForgeryModRegistered  + GenericHelpers.GetAttribute<Mod>(type).ModId);

                _registeredMods.Add(GenericHelpers.GetAttribute<Mod>(type).ModId, info);
            }
        }

        /// <summary>
        ///     Gets a list of mods from the given assembly
        /// </summary>
        /// <param name="assembly">The assembly from which the mods will be registered</param>
        /// <returns>An array of all found mods converted to ModInfo objects</returns>
        public ModInfo[] GetMods(Assembly assembly)
        {
            List<ModInfo> modInfos = new List<ModInfo>();

            foreach (Type type in GenericHelpers.GetTypesWithAttribute(typeof(Mod), assembly))
            {
                ModInfo info = TypeToModInfo(type);

                Logging.Info(Language.ForgeryModRegistered + GenericHelpers.GetAttribute<Mod>(type).ModId);

                _registeredMods.Add(GenericHelpers.GetAttribute<Mod>(type).ModId, info);

                modInfos.Add(info);
            }

            return modInfos.ToArray();
        }

        /// <summary>
        ///     Converts a type to a modInfo
        /// </summary>
        /// <param name="type">Converts a given type to a modInfo Class</param>
        /// <returns> A ModInfo object containing the neccesairy mod data</returns>
        public ModInfo TypeToModInfo(Type type)
        {
            GameObject modObject = new GameObject(GenericHelpers.GetAttribute<Mod>(type).ModId);

            ModInfo info = new ModInfo
            {
                ModClass = modObject.AddComponent(type),
                PreInit = type.GetMethod(InternalData.PRE_INIT),
                Load = type.GetMethod(InternalData.LOAD),
                PostInit = type.GetMethod(InternalData.POST_INIT),
                ModClassType = type
            };

            modObject.transform.SetParent(transform.parent);

            return info;
        }

        /// <summary>
        ///     Mod. PreInit Function. 
        ///     Fires the preInit function on all registered mods.
        /// </summary>
        public void ModsPreInit()
        {
            foreach (KeyValuePair<string, ModInfo> info in _registeredMods)
            {
                if (info.Value.PreInit == null || info.Value.PreInitDone)
                {
                    continue;
                }

                info.Value.PreInitDone = true;

                Logging.Info(Language.ForgeryModPreInitFired + info.Key);

                info.Value.PreInit.Invoke(info.Value.ModClass, new object[] { });
            }
        }

        /// <summary>
        ///     Mods Loading function.
        ///     Fires the Load function on all registered mods.
        /// </summary>
        public void ModsLoad()
        {
            foreach (KeyValuePair<string, ModInfo> info in _registeredMods)
            {
                if (info.Value.PreInit == null || info.Value.LoadDone)
                {
                    continue;
                }

                Logging.Info(Language.ForgeryModLoadFired + info.Key);

                info.Value.LoadDone = true;
                info.Value.Load.Invoke(info.Value.ModClass, new object[] { });
            }
        }

        /// <summary>
        ///     Mods PostInit function.
        ///     Fires the PostInit function on all registered mods.
        /// </summary>
        public void ModsPostInit()
        {
            foreach (KeyValuePair<string, ModInfo> info in _registeredMods)
            {
                if (info.Value.PreInit == null || info.Value.PostInitDone)
                {
                    continue;
                }

                Logging.Info(Language.ForgeryModPostInitFired + info.Key);

                info.Value.PostInitDone = true;
                info.Value.PostInit.Invoke(info.Value.ModClass, new object[] { });
            }
        }
    }
}