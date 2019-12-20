using System;
using System.Collections.Generic;
using Forge.Errors;
using Forge.Settings;
using Forge.Utils;
using UnityEngine;

namespace Forge.Anvil.Registry
{
    
    [Serializable] public class DictionaryOfStringAndObject : SerializableDictionary<string, object> {}
    /// <inheritdoc cref="IRegistry" />
    public class Registry : MonoBehaviour, IRegistry
    {

        /// <summary>
        ///     Dictionary containing all currently registered objects.
        /// </summary>
        [SerializeField]
        public DictionaryOfStringAndObject ItemRegister = new DictionaryOfStringAndObject();

        /// <inheritdoc />
        public T2 GetItem<T2>(string id)
        {
            object objectItem = ItemRegister[id];

            T2 item;
            
            if(objectItem as GameObject)
            {
                item = Instantiate(ItemRegister[id] as GameObject).GetComponent<T2>();   
            }
            else
            {
                item = ItemRegister[id] is T2 ? (T2) ItemRegister[id] : default;
            }

            if (item == null)
            {
                Logging.Warning(Language.RegistryItemNotFound + id);
            }

            return item;
        }

        /// <inheritdoc />
        public GameObject GetItem(string id)
        {
            GameObject item = Instantiate(ItemRegister[id] as GameObject).gameObject;

            if (item == null)
            {
                Logging.Warning(Language.RegistryItemNotFound + id);
            }

            return item;
        }

        /// <inheritdoc />
        public Type GetType(string id)
        {
            if (!ItemRegister.ContainsKey(id))
            {
                Logging.Warning(Language.RegistryItemNotFound + id);
                return null;
            }

            return ItemRegister[id] as Type;
        }

        /// <inheritdoc />
        public bool Register(string id, GameObject item)
        {
            if (ItemRegister.ContainsKey(id))
            {
                Logging.Warning(Language.RegistryItemRegistered + id);
                return false;
            }
            
            item.transform.parent = transform;
            item.SetActive(false);
            ItemRegister.Add(id, item);

            return true;
        }

        /// <inheritdoc />
        public bool RegisterType<T1>(string id)
        {
            if (ItemRegister.ContainsKey(id))
            {
                Logging.Warning(Language.RegistryItemRegistered + id);
                return false;
            }
            
            ItemRegister.Add(id, typeof(T1));

            return true;
        }

        /// <inheritdoc />
        public bool Delete(string id)
        {
            if (!ItemRegister.ContainsKey(id))
            {
                Logging.Warning(Language.RegistryDeleteItemNotFound + id);

                return false;
            }

            ItemRegister.Remove(id);
            return true;
        }
    }
}
