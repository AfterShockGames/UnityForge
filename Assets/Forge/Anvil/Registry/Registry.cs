using System;
using System.Collections.Generic;
using Forge.Pooling;
using Forge.Errors;
using Forge.Settings;
using UnityEngine;

namespace Forge.Anvil.Registry
{

    /// <inheritdoc />
    public class Registry<T> : IRegistry
    {
        /// <summary>
        ///     Dictionary containing all currently registered objects.
        /// </summary>
        private readonly Dictionary<string, object> _register = new Dictionary<string, object>();

        /// <inheritdoc />
        public T2 GetItem<T2>(string id)
        {
            T2 item = PoolingManager.Instance.Spawn(id).GetComponent<T2>();

            if (item == null)
            {
                Logging.Warning(Language.RegistryItemNotFound + id);
            }

            return item;
        }

        /// <inheritdoc />
        public GameObject GetItem(string id)
        {
            GameObject item = PoolingManager.Instance.Spawn(id);

            if (item == null)
            {
                Logging.Warning(Language.RegistryItemNotFound + id);
            }

            return item;
        }

        /// <inheritdoc />
        public Type GetType(string id)
        {
            if (!_register.ContainsKey(id))
            {
                Logging.Warning(Language.RegistryItemNotFound + id);
                return null;
            }

            return (Type)_register[id];
        }

        /// <inheritdoc />
        public bool Register(string id, object item, int amount = 5)
        {
            if (_register.ContainsKey(id))
            {
                Logging.Warning(Language.RegistryItemRegistered + id);
                return false;
            }

            //Check if object is a GameObject. If it is one create a Pool.
            GameObject newItem = item as GameObject;
            if (newItem != null)
            {
                PoolingManager.Instance.CreateRepository(id, amount, newItem);
            }

            _register.Add(id, item);

            return true;
        }

        /// <inheritdoc />
        public bool RegisterType<T1>(string id)
        {
            return Register(id, typeof(T1));
        }

        /// <inheritdoc />
        public bool Delete(string id)
        {
            if (!_register.ContainsKey(id))
            {
                Logging.Warning(Language.RegistryDeleteItemNotFound + id);

                return false;
            }

            PoolingManager.Instance.RemoveRepository(id);

            _register.Remove(id);
            return true;
        }
    }
}
