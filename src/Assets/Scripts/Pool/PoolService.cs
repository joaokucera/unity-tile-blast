using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Addressable;
using Board;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Pool
{
    public class PoolService : IPoolService, IInitializable, IDisposable
    {
        readonly PoolModel _poolModel;
        readonly IPoolFactory _poolFactory;
        readonly IAddressableService _addressableService;

#if UNITY_EDITOR
        Transform _parentTransform;
#endif

        public PoolService(PoolModel poolModel, IPoolFactory poolFactory, IAddressableService addressableService)
        {
            _poolModel = poolModel;
            _poolFactory = poolFactory;
            _addressableService = addressableService;
        }

        public void Initialize()
        {
#if UNITY_EDITOR
            _parentTransform = new GameObject("PoolServiceParent").transform;
            Object.DontDestroyOnLoad(_parentTransform);
#endif
        }

        public void Dispose()
        {
            _poolModel.PoolsByAssetName.Clear();

#if UNITY_EDITOR
            if (_parentTransform == null)
            {
                return;
            }

            if (Application.isPlaying)
            {
                Object.Destroy(_parentTransform.gameObject);
            }
            else
            {
                Object.DestroyImmediate(_parentTransform.gameObject);
            }
#endif
        }

        Stack<GameObject> GetPool(string assetName)
        {
            _poolModel.PoolsByAssetName.TryGetValue(assetName, out var stack);

            if (stack != null)
            {
                return stack;
            }

            stack = new Stack<GameObject>();
            _poolModel.PoolsByAssetName.Add(assetName, stack);

            return stack;
        }

        GameObject GetPooledObjectInternal(string assetName, GameObject prefab, Transform parent = null)
        {
            var stack = GetPool(assetName);
            GameObject pooledObject;

            if (stack.Count == 0)
            {
                var poolView = _poolFactory.CreatePoolView(prefab);
                poolView.Init(this, assetName);

                pooledObject = poolView.gameObject;
            }
            else
            {
                pooledObject = stack.Pop();
            }

            pooledObject.SetActive(true);

            if (parent != null)
            {
                pooledObject.transform.SetParent(parent, false);
            }

            return pooledObject;
        }

        GameObject GetPooledObjectInternal(GameObject prefab, Transform parent = null)
        {
            return GetPooledObjectInternal(prefab.name, prefab, parent);
        }

        public async Task<GameObject> GetPooledObjectAsync(IBoardTileDefinition boardTileDefinition, Transform parent = null)
        {
            var prefab = await _addressableService.LoadAssetAsync<GameObject>(boardTileDefinition.AssetReference);

            return GetPooledObjectInternal(prefab, parent);
        }

        public void ReturnPooledObject(GameObject objectToReturn)
        {
            if (objectToReturn == null)
            {
                return;
            }

            var poolMember = objectToReturn.GetComponentInChildren<PoolView>(true);

            if (poolMember != null)
            {
                poolMember.ReturnToPool();
            }
        }

        public void ReturnPooledObject(string assetName, GameObject objectToReturn)
        {
            objectToReturn.SetActive(false);

#if UNITY_EDITOR
            objectToReturn.transform.SetParent(_parentTransform, false);
#else
            objectToReturn.transform.SetParent(null, false);
#endif

            var stack = GetPool(assetName);
            stack.Push(objectToReturn);
        }

        public void ReturnAllPooledChildren(Transform parent)
        {
            if (parent == null)
            {
                return;
            }

            var pooledObjects = parent.GetComponentsInChildren<PoolView>(true);

            // Returning in the same order they were created
            for (int i = pooledObjects.Length - 1; i >= 0; i--)
            {
                pooledObjects[i].ReturnToPool();
            }
        }
    }
}
