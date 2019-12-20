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
        /// <typeparam name="T1">The component to get</typeparam>
        /// <param name="id">The Item ID</param>
        /// <returns>The resulting MonoBehaviour</returns>
        T1 GetItem<T1>(string id);

        /// <summary>
        ///     Spawns a new instance from the pooling manager and returns the object
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
        ///     If amount is specified there will be an item pool created
        /// </summary>
        /// <param name="item">Registry ID</param>
        /// <param name="gameObject">The object to register</param>
        /// <returns>Succes</returns>
        bool Register(string id, GameObject item);

        /// <summary>
        ///     Registers a type to the registry
        /// </summary>
        /// <typeparam name="T2">The type to register</typeparam>
        /// <param name="id">The item identifier</param>
        /// <returns>Success</returns>
        bool RegisterType<T2>(string id);

        /// <summary>
        ///     Deletes a registry item by the Registry ID specified
        /// </summary>
        /// <param name="id">The Registry ID</param>
        /// <returns>Success</returns>
        bool Delete(string id);
    }
}
