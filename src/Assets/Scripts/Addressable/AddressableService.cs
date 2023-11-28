using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Addressable
{
    public class AddressableService : IAddressableService
    {
        readonly AddressableModel _addressableModel;

        public AddressableService(AddressableModel addressableModel)
        {
            _addressableModel = addressableModel;
        }

        public async Task<T> LoadAssetAsync<T>(AssetReference assetReference) where T : Object
        {
            ValidateRuntimeKeyIsValid(assetReference);

            object assetKey = assetReference.RuntimeKey;

            if (!_addressableModel.LoadedAssetsByAssetKey.TryGetValue(assetKey, out var obj))
            {
                obj = await LoadAssetAsyncInternal<T>(assetKey);
            }

            return obj as T;
        }

        public async Task<SceneInstance> LoadSceneAsync(AssetReference assetReference,
            LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true, int priority = 100)
        {
            ValidateRuntimeKeyIsValid(assetReference);

            object assetKey = assetReference.RuntimeKey;

            if (_addressableModel.SceneInstancesByAssetKey.TryGetValue(assetKey, out var sceneInstance))
            {
                throw new Exception($"Scene {assetKey} is already loaded");
            }

            var asyncOperationHandle = Addressables.LoadSceneAsync(assetKey, loadMode, activateOnLoad, priority);
            sceneInstance = await asyncOperationHandle.Task;

            if (asyncOperationHandle.Status == AsyncOperationStatus.Failed)
            {
                throw new Exception($"Failed to load scene {assetKey}");
            }

            _addressableModel.SceneInstancesByAssetKey[assetKey] = sceneInstance;

            return sceneInstance;
        }

        public async Task<SceneInstance> UnloadSceneAsync(AssetReference assetReference, bool autoReleaseHandle = true)
        {
            ValidateRuntimeKeyIsValid(assetReference);

            object assetKey = assetReference.RuntimeKey;

            if (!TryRemoveScene(assetKey, out var sceneInstance))
            {
                throw new Exception($"Scene {assetKey} cannot be unloaded as it was not loaded");
            }

            var asyncOperationHandle = Addressables.UnloadSceneAsync(sceneInstance, autoReleaseHandle);
            sceneInstance = await asyncOperationHandle.Task;

            return sceneInstance;
        }

        async Task<Object> LoadAssetAsyncInternal<T>(object assetKey) where T : Object
        {
            var asyncOperationHandle = Addressables.LoadAssetAsync<T>(assetKey);
            var loadedAsset = await asyncOperationHandle.Task;

            if (asyncOperationHandle.Status == AsyncOperationStatus.Failed)
            {
                throw new Exception($"Failed to load asset {assetKey}");
            }

            _addressableModel.LoadedAssetsByAssetKey[assetKey] = loadedAsset;
            return loadedAsset;
        }

        bool TryRemoveScene(object assetKey, out SceneInstance sceneInstance)
        {
            return _addressableModel.SceneInstancesByAssetKey.TryGetValue(assetKey, out sceneInstance)
                   && _addressableModel.SceneInstancesByAssetKey.Remove(assetKey);
        }

        static void ValidateRuntimeKeyIsValid(IKeyEvaluator assetReference)
        {
            if (!assetReference.RuntimeKeyIsValid())
            {
                throw new NullReferenceException("AssetReference has no runtime key defined");
            }
        }
    }
}
