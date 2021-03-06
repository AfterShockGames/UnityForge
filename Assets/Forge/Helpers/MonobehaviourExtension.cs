using System;
using Forge.Anvil.Registry;
using Forge.Settings;
using UnityEngine;
using Object = UnityEngine.Object;

public static class MonobehaviourExtension
{
    /// <summary>
    ///     Gets a registry by identifier
    /// </summary>
    /// <param name="monoBehaviour"></param>
    /// <param name="registryIdentifier">The registry name</param>
    /// <returns>The found Registry</returns>
    public static IRegistry GetRegistry(this MonoBehaviour monoBehaviour, string registryIdentifier)
    {
        return Register.GetRegister.GetRegistry(registryIdentifier);
    }

    /// <summary>
    ///     Registers an item to the Registry
    /// </summary>
    /// <param name="monoBehaviour"></param>
    /// <param name="registryIdentifier">The registry identifier</param>
    /// <param name="itemIdentifier">The item Identifier</param>
    /// <param name="item">The item to register</param>
    /// <param name="amount">The pool amount</param>
    public static void RegisterItem(this MonoBehaviour monoBehaviour, string registryIdentifier, string itemIdentifier, object item, int amount = 5)
    {
        Register.GetRegister.RegisterItem(registryIdentifier, itemIdentifier, item);
    }

    /// <summary>
    ///     Registers an item to the global Forge Registry
    /// </summary>
    /// <param name="monoBehaviour"></param>
    /// <param name="itemIdentifier">The unique item identifier which is going to be registered</param>
    /// <param name="item">The item to register</param>
    /// <param name="amount">The amount to create by default</param>
    public static void RegisterGlobalItem(this MonoBehaviour monoBehaviour, string itemIdentifier, object item, int amount = 5)
    {
        Register
            .GetRegister
            .RegisterItem(InternalData.FORGE_REGISTRY, itemIdentifier, item);
    }

    /// <summary>
    ///     Gets an item from a given Registry and returns the component
    /// </summary>
    /// <typeparam name="T">The registry/item type</typeparam>
    /// <param name="monoBehaviour"></param>
    /// <param name="registeryIdentifier">The registry to search in</param>
    /// <param name="itemIdentifier">The item to get</param>
    /// <returns>The c</returns>
    public static T GetItem<T>(this MonoBehaviour monoBehaviour, string registeryIdentifier, string itemIdentifier) 
        where T : MonoBehaviour
    {
        return Register
            .GetRegister
            .GetRegistry(registeryIdentifier)
            .GetItem<T>(itemIdentifier);
    }

    /// <summary>
    ///     Gets a item from the Registry as a Type.
    /// </summary>
    /// <param name="monoBehaviour"></param>
    /// <param name="registryIdentifier">The registry to search in</param>
    /// <param name="itemIdentifier">The item to get</param>
    /// <returns>The item Type</returns>
    public static Type GetItem(this MonoBehaviour monoBehaviour, string registryIdentifier, string itemIdentifier)
    {
        return Register
            .GetRegister
            .GetRegistry(registryIdentifier)
            .GetType(itemIdentifier);
    }

    /// <summary>
    ///     Gets an item from the global Forge Registry and returns the component by the specified type
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <param name="monoBehaviour"></param>
    /// <param name="itemIdentifier">The item to search for</param>
    /// <returns>A new instance of the item</returns>
    public static T GetGlobalItem<T>(this MonoBehaviour monoBehaviour, string itemIdentifier) 
        where T : MonoBehaviour
    {
        return Register
            .GetRegister
            .GetRegistry(InternalData.FORGE_REGISTRY)
            .GetItem<T>(itemIdentifier);
    }

    /// <summary>
    ///     Registers a MonoBehaviour component
    /// </summary>
    /// <typeparam name="T">The component to register</typeparam>
    /// <param name="monoBehaviour"></param>
    /// <param name="componentIdentifier">The unique component identifier</param>
    public static void RegisterForgeComponent<T>(this MonoBehaviour monoBehaviour, string componentIdentifier) 
        where T : MonoBehaviour
    {
        Register
            .GetRegister
            .GetRegistry(InternalData.FORGE_COMPONENT_REGISTRY)
            .RegisterType<T>(componentIdentifier);
    }

    /// <summary>
    ///     Adds the component from the Forge component registry to the gameObject on this MonoBehavior.
    ///     And casts it to the given Type param
    /// </summary>
    /// <typeparam name="T">Type to cast to</typeparam>
    /// <param name="monoBehaviour"></param>
    /// <param name="componentIdentifier">The component identifier</param>
    /// <returns>The added component</returns>
    public static T AddComponent<T>(this MonoBehaviour monoBehaviour, string componentIdentifier)
        where T : MonoBehaviour
    {
        return (T) monoBehaviour.gameObject.AddComponent(monoBehaviour.GetForgeComponent(componentIdentifier));
    }

    /// <summary>
    ///     Gets a Forge component Type
    /// </summary>
    /// <param name="monoBehaviour"></param>
    /// <param name="componentIdentifier">The component identifier</param>
    /// <returns>The component type</returns>
    public static Type GetForgeComponent(this MonoBehaviour monoBehaviour, string componentIdentifier)
    {
        return Register
            .GetRegister
            .GetRegistry(InternalData.FORGE_COMPONENT_REGISTRY)
            .GetType(componentIdentifier);
    }

    /// <summary>
    ///     ***OBSOLETE***
    ///     Creates a Forge gameObject which is ready for being registered in any registry
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <param name="monoBehaviour"></param>
    /// <param name="itemId">The itemIdentifier</param>
    /// <param name="resourceUrl">The resources url to load the object from</param>
    /// <returns>
    ///     The newly generated item.
    /// </returns>
    [Obsolete]
    public static GameObject InitializeForgeGameObject<T>(this MonoBehaviour monoBehaviour, string itemId, string resourceUrl) 
        where T : MonoBehaviour
    {
        GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>(resourceUrl));
        gameObject.name = itemId;

        gameObject.AddComponent<T>();
        gameObject.SetActive(false);
        gameObject.hideFlags = HideFlags.HideInHierarchy;

        Object.DontDestroyOnLoad(gameObject);

        return gameObject;
    }

    /// <summary>
    ///     ***OBSOLETE***
    ///     Converts a GameObject to a ForgeGameObject
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <param name="monoBehaviour"></param>
    /// <param name="itemId">Item Identifier</param>
    /// <param name="gameObject">GameObject to modify</param>
    /// <returns>
    ///     Modified GameObject
    /// </returns>
    [Obsolete]
    public static GameObject InitializeForgeGameObject<T>(this MonoBehaviour monoBehaviour, string itemId, GameObject gameObject) 
        where T : MonoBehaviour
    {
        gameObject.name = itemId;

        gameObject.AddComponent<T>();
        gameObject.SetActive(false);
        gameObject.hideFlags = HideFlags.HideInHierarchy;

        Object.DontDestroyOnLoad(gameObject);

        return gameObject;
    }

    /// <summary>
    ///     ***OBSOLETE***
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <param name="monoBehaviour"></param>
    /// <param name="itemId">Item Identifier</param>
    /// <param name="objectId"></param>
    /// <param name="registeryId"></param>
    /// <returns>
    ///     Modified GameObject
    /// </returns>
    [Obsolete]
    public static GameObject InitializeForgeGameObject<T>(this MonoBehaviour monoBehaviour, string itemId, string objectId, string registeryId) where T : MonoBehaviour, new()
    {
        Debug.Log(monoBehaviour);
        GameObject gameObject = monoBehaviour.GetItem<T>(registeryId, objectId).gameObject;
        gameObject.name = itemId;

        gameObject.AddComponent<T>();
        gameObject.SetActive(false);
        gameObject.hideFlags = HideFlags.HideInHierarchy;

        Object.DontDestroyOnLoad(gameObject);

        return gameObject;
    }
}