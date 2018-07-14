using UnityEngine;
using System.Reflection;
using Forge.Settings;
using Forge.Anvil;
using Forge.Anvil.Registry;

namespace Forge
{
    /// <summary>
    ///     Basic unity component which initializes and runs the Forge framework.
    ///     This class should be overriden and the Forge registries should be created in the BeforeModLoading function.
    /// </summary>
    public abstract class ForgeInitializer : MonoBehaviour
    {

        /// <summary>
        ///     Starts the initialization of Forge.
        /// </summary>
        public void Start()
        {
            DontDestroyOnLoad(gameObject);

            InitForge();
            BeforeModLoading();
            StartModLoading();
        }

        /// <summary>
        ///     Creates and positions all Forge objects.
        /// </summary>
        private void InitForge()
        {
            //TODO::Check if assembly is correct
            InternalData.MasterAssembly = Assembly.GetExecutingAssembly();

            Register.GetRegister.transform.SetParent(gameObject.transform);
            Hammer.Hammer.GetHammer.transform.SetParent(gameObject.transform);
            AnvilRegistry.GetAnvil.transform.SetParent(gameObject.transform);
        }

        /// <summary>
        ///     Starts the modloading sequence, be sure to create all game item registries first.
        /// </summary>
        private static void StartModLoading()
        {
            //Get and load all mods in the Anvil Registry
            AnvilRegistry.GetAnvil.GetMods();

            AnvilRegistry.GetAnvil.ModsPreInit();
            AnvilRegistry.GetAnvil.ModsLoad();
            AnvilRegistry.GetAnvil.ModsPostInit();
        }

        /// <summary>
        ///     This function can be used to create any registries or edit any other Forge logic before mod loading
        /// </summary>
        public abstract void BeforeModLoading();
    }
}
