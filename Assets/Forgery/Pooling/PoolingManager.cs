using System;
using UnityEngine;
using System.Linq;
using Forgery.Hammer.Events;
using Forgery.Utils;

namespace Forgery.Pooling
{
    [Obsolete]
    public class PoolingManager : Singleton<PoolingManager>
    {
        private readonly EventList<PoolingRepository> _repositories = new EventList<PoolingRepository>();

        /// <summary>
        /// retrieve all repositories
        /// </summary>
        /// <returns>All repositories</returns>
        public EventList<PoolingRepository> GetRepositories()
        {
            return _repositories;
        }

        /// <summary>
        /// Retries a gameobject repository by id
        /// </summary>
        /// <param name="id">id of the gameobject that the repository manages</param>
        /// <returns>target repository or null</returns>
        public virtual PoolingRepository GetRepository(string id)
        {
            return _repositories.FirstOrDefault(repository => repository.Id == id);
        }

        /// <summary>
        /// Creates a repository if it does not exist
        /// </summary>
        /// <param name="id">Id for the gameobject repository</param>
        /// <param name="instancesToPreallocate">Amount of instances to directly instantiate</param>
        /// <param name="targetGameObject">The target gameobject that this repository should manage</param>
        /// <returns>Existing or new repository</returns>
        public virtual PoolingRepository CreateRepository(string id, int instancesToPreallocate, GameObject targetGameObject)
        {
            var repository = this.GetRepository(id);

            if (repository == null)
            {
                repository = new PoolingRepository();

                repository.SetInstancesToPreallocate(instancesToPreallocate)
                    .SetManager(this)
                    .SetId(id)
                    .SetTargetGameObject(targetGameObject)
                    .Initialize();

                _repositories.Add(repository);
            }

            return repository;
        }


        /// <summary>
        /// stops managing the repository optionally destroying all managed objects
        /// </summary>
        /// <param name="id">PoolingRepository object id</param>
        /// <param name="shouldDestroyAllManagedObjects">If set to <c>true</c> should destroy all managed objects.</param>
        public virtual void RemoveRepository(string id, bool shouldDestroyAllManagedObjects = true)
        {
            var targetRepository = _repositories.FirstOrDefault(repository => repository.Id == id);

            if (targetRepository != null)
            {
                targetRepository.clearStack(shouldDestroyAllManagedObjects);
                _repositories.Remove(targetRepository);
            }
        }

        /// <summary>
        /// if repository exists we spawn an object, otherwise we return null
        /// </summary>
        /// <param name="_id">Game object instance identifier.</param>
        /// <param name="position">Game object target position.</param>
        /// <param name="rotation">Game object target rotation.</param>
        public virtual GameObject Spawn(string _id, 
            Vector3 position = default(Vector3),
            Quaternion rotation = default(Quaternion))
        {
            var repository = this.GetRepository(_id);

            if (repository != null)
            {
                GameObject gameObject = null;

                gameObject = repository.Spawn();

                var newTransform = gameObject.transform;

                newTransform.parent = null;

                newTransform.position = position;
                newTransform.rotation = rotation;

                gameObject.SetActive(true);

                return gameObject;
            }

            return null;
        }

        /// <summary>
        /// Recycles the GameObject back into its repository. If the GameObject has repository it is destroyed.
        /// </summary>
        /// <param name="id">The id of the gameobject repository</param>
        /// <param name="instance">The instance to despawn.</param>
        public virtual void Despawn(string id, GameObject instance)
        {
            if (instance == null)
                return;

            var repository = GetRepository(id);
            if (repository == null)
            {
                Destroy(instance);
            }
            else
            {
                repository.Despawn(instance);
            }
        }
    }
}