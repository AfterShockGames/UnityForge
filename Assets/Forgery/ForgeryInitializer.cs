using UnityEngine;
using System.Reflection;
using Forgery.Settings;
using Forgery.Anvil;
using Forgery.Anvil.Registry;

namespace Forgery
{
    /// <summary>
    ///     Basic unity component which initializes and runs the forgery framework.
    ///     This class should be overriden and the Forgery registries should be created in the BeforeModLoading function.
    /// </summary>
    public abstract class ForgeryInitializer : MonoBehaviour
    {

        /// <summary>
        ///     Starts the initialization of Forgery.
        /// </summary>
        public void Start()
        {
            DontDestroyOnLoad(gameObject);

            InitForgery();
            BeforeModLoading();
            StartModLoading();
        }

        /// <summary>
        ///     Creates and positions all forgery objects.
        /// </summary>
        private void InitForgery()
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
        ///     This function can be used to create any registries or edit any other Forgery logic before mod loading
        /// </summary>
        public abstract void BeforeModLoading();
    }
}
