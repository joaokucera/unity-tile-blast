using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Addressable
{
    public interface IAddressableService
    {
        Task<T> LoadAssetAsync<T>(AssetReference assetReference) where T : Object;

        Task<SceneInstance> LoadSceneAsync(AssetReference assetReference,
            LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true, int priority = 100);

        Task<SceneInstance> UnloadSceneAsync(AssetReference assetReference, bool autoReleaseHandle = true);
    }
}
