using UnityEngine;
using System.Collections.Generic;
using Forge.Errors;
using Forge.Settings;

namespace Forge.Anvil.Registry
{
    /// <summary>
    ///     Forge Register class. This is the base class for creating registries.
    ///     TODO::Registry accessibility. Currently modders can edit core registries 
    /// </summary>
    public class Register : MonoBehaviour
    {
        /// <summary>
        ///     Gets the current register
        /// </summary>
        public static Register GetRegister
        {
            get
            {
                return _register ? _register : InstantiateRegister;
            }
        }

        /// <summary>
        ///     Creates the register
        /// </summary>
        public static Register InstantiateRegister
        {
            get
            {
                GameObject go = new GameObject(InternalData.ANVIL_REGISTER);
                _register = go.AddComponent<Register>();


                _register.CreateRegistry(InternalData.FORGE_REGISTRY);
                _register.CreateRegistry(InternalData.FORGE_COMPONENT_REGISTRY);

                return _register;
            }
        }

        /// <summary>
        ///     The register
        /// </summary>
        private static Register _register;

        /// <summary>
        ///     A dictionary holding all registries
        /// </summary>
        private readonly Dictionary<string, IRegistry> _registerList = new Dictionary<string, IRegistry>();

        /// <summary>
        ///     Gets a generic registry 
        /// </summary>
        /// <param name="registryIdentifier">The registryIdentifier</param>
        /// <returns>A generic registry</returns>
        public IRegistry GetRegistry(string registryIdentifier)
        {
            if (!_registerList.ContainsKey(registryIdentifier))
            {
                Debug.Log("Registry not found: " + registryIdentifier);
                return null;
            }

            return _registerList[registryIdentifier];
        }

        /// <summary>
        ///     Registers a gameObject 
        /// </summary>
        /// <param name="registryName">The lists name</param>
        /// <param name="itemId">The item ID</param>
        /// <param name="item">The item</param>
        /// <param name="amount">The pool amount</param>
        public void RegisterItem(string registryName, string itemId, object item, int amount = 5)
        {
            if (!_registerList.ContainsKey(registryName))
            {
                CreateRegistry(registryName);
            }

            IRegistry registerList = _registerList[registryName];

            if (registerList != null)
            {
                registerList.Register(itemId, item, amount);
            }
        }

        /// <summary>
        ///     Creates a new registry list
        /// </summary>
        /// <param name="registryName">registryName</param>
        /// <returns>The created registry</returns>
        public IRegistry CreateRegistry(string registryName)
        {
            if (_registerList.ContainsKey(registryName))
            {
                return null;
            }

            GameObject registryGo = new GameObject(registryName);
            registryGo.transform.parent = transform;
            IRegistry registry = registryGo.AddComponent<Registry>();

            _registerList.Add(registryName, registry);

            return registry;
        }

        /// <summary>
        ///     Creates a new registry list with a required Type
        /// </summary>
        /// <typeparam name="T">The registries type</typeparam>
        /// <param name="registryName">registryName</param>
        /// <returns>The created registry</returns>
        public IRegistry CreateRegistry<T>(string registryName)
        {
            if (_registerList.ContainsKey(registryName))
            {
                throw new ForgeCritical(Language.ForgeCriticalRegistryAlreadyExists);
            }
            
            GameObject registryGo = new GameObject(registryName);
            registryGo.transform.parent = transform;
            IRegistry registry = registryGo.AddComponent<Registry>();

            _registerList.Add(registryName, registry);

            return registry;
        }
    }
}