using System;
using UnityEngine;
using System.Collections.Generic;

namespace Forge.Pooling
{
    [Obsolete]
    public class PoolingRepository
    {
        private string _id;
        private GameObject _targetGameObject;
        private PoolingManager _poolingManager;

        private Stack<GameObject> _gameObjects;

        private int _spawnedInstanceCount = 0;
        private int _instancesToAllocateIfEmpty = 1;
        private int _instancesToPreallocate = 10;

        internal string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Sets the poolingManager. Normally there's only 1 poolingManager but optionally there can be more.
        /// </summary>
        /// <param name="poolingManager">The poolingManager that manages this repository</param>
        /// <returns>The current repository</returns>
        public virtual PoolingRepository SetManager(PoolingManager poolingManager)
        {
            _poolingManager = poolingManager;
            return this;
        }

        /// <summary>
        /// preps the Stack and does preallocation
        /// </summary>
        public virtual PoolingRepository Initialize()
        {
            //prefab.prefabPoolName = prefab.gameObject.name;
            _gameObjects = new Stack<GameObject>();
            allocateGameObjects(_instancesToPreallocate);

            return this;
        }

        public virtual PoolingRepository SetId(string id)
        {
            _id = id;
            return this;
        }

        /// <summary>
        /// Only set this on initialize otherwise you might loose items /effects during gameplay
        /// </summary>
        /// <param name="initialSize"></param>
        /// <returns></returns>
        public virtual PoolingRepository SetInstancesToAllocateIfEmpty(int instancesToAllocateIfEmpty)
        {
            if (instancesToAllocateIfEmpty != 0)
            {
                _instancesToAllocateIfEmpty = instancesToAllocateIfEmpty;
            }

            return this;
        }

        public virtual PoolingRepository SetInstancesToPreallocate(int instancesToPreallocate)
        {
            _instancesToPreallocate = instancesToPreallocate;
            return this;
        }

        /// <summary>
        /// Can only be done once, otherwise we get a difference between the id and the targetGameObject, and the actual pooled gameobjects
        /// </summary>
        /// <param name="targetGameObject"></param>
        /// <returns></returns>
        public virtual PoolingRepository SetTargetGameObject(GameObject targetGameObject)
        {
            if (_targetGameObject == null)
            {
                _targetGameObject = targetGameObject;
            }

            return this;
        }

        /// <summary>
        /// Allocates new gameobjects by amount
        /// </summary>
        /// <param name="amountToAllocate">The amount of gameobjects to allocate in the stack</param>
        private void allocateGameObjects(int amountToAllocate)
        {
            for (int i = 0; i < amountToAllocate; i++)
            {
                GameObject instance = GameObject.Instantiate(_targetGameObject) as GameObject;

#if UNITY_4_6 || UNITY_5_0

            if(go.transform as RectTransform)
            {
                instance.transform.SetParent(_poolingManager.transform, false);
            }
            else
#endif
                instance.transform.parent = _poolingManager.transform;

                instance.SetActive(false);
                _gameObjects.Push(instance);
            }
        }

        /// <summary>
        /// pops an object off the stack. Returns null if we hit the hardLimit.
        /// </summary>
        private GameObject pop()
        {
            if (_gameObjects.Count > 0)
            { 
                _spawnedInstanceCount++;
                return _gameObjects.Pop();
            }

            allocateGameObjects(_instancesToAllocateIfEmpty);
            return pop();
        }

        /// <summary>
        /// fetches a new instance from the recycle bin. Returns null if we reached the hardLimit.
        /// </summary>
        public virtual GameObject Spawn()
        {
            var go = pop();

            return go;
        }


        /// <summary>
        /// returns an instance to the recycle bin
        /// </summary>
        /// <param name="go">Go.</param>
        public virtual void Despawn(GameObject instance)
        {
#if UNITY_4_6 || UNITY_5_0

            if(go.transform as RectTransform)
            {
                instance.transform.SetParent(_poolingManager.transform, false);
            }
            else
#endif
            instance.transform.parent = _poolingManager.transform;

            instance.SetActive(false);

            _spawnedInstanceCount--;
            _gameObjects.Push(instance);
        }


        /// <summary>
        /// clears out the bin optionally calling Destroy on all objects in it. note than any spawned objects are not touched by this operation!
        /// </summary>
        public virtual void clearStack(bool shouldDestroyAllManagedObjects)
        {
            while (_gameObjects.Count > 0)
            {
                var instance = _gameObjects.Pop();

                if (shouldDestroyAllManagedObjects)
                    GameObject.Destroy(instance);
            }
        }
    }
}