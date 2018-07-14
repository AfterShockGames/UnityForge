using System;
using UnityEngine;

namespace Forge.Anvil.Registry
{
    /// <summary>
    ///     Registry interface
    /// </summary>
    public interface IRegistry
    {
        /// <summary>
        ///     Spawns a new instance from the pooling manager and gets the component specified
        /// </summary>
        /// <typeparam name="T">The component to get</typeparam>
        /// <param name="id">The Item ID</param>
        /// <returns>The resulting MonoBehaviour</returns>
        T GetItem<T>(string id);

        /// <summary>
        ///     Spawns a new isntance from the pooling manager and returns the object
        /// </summary>
        /// <param name="id">The Item ID</param>
        /// <returns></returns>
        GameObject GetItem(string id);

        /// <summary>
        ///     Returns the registered type at the given ID.
        ///     Usefull for registering components
        /// </summary>
        /// <param name="id">The Item ID</param>
        /// <returns>The Requested Type</returns>
        Type GetType(string id);

        /// <summary>
        ///     Registers an item with the specified Registry ID.
        ///     If the item is a GameObject there will be a pool created for it.
        /// </summary>
        /// <param name="id">Registry ID</param>
        /// <param name="item">Item, This should inherit from MonoBehaviour</param>
        /// <param name="amount">
        ///     The amount of items the PoolingManager should create in advance.
        ///     Set this to 0 if you want to create objects on demand
        /// </param>
        /// <returns>Succes</returns>
        bool Register(string id, object item, int amount = 5);

        /// <summary>
        ///     Registers a type to the registry
        /// </summary>
        /// <typeparam name="T">The type to register</typeparam>
        /// <param name="id">The item identifier</param>
        /// <returns>Success</returns>
        bool RegisterType<T>(string id);

        /// <summary>
        ///     Deletes a registry item by the Registry ID specified
        /// </summary>
        /// <param name="id">The Registry ID</param>
        /// <returns>Success</returns>
        bool Delete(string id);
    }
}
