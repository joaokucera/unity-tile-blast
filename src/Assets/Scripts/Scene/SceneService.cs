using System.Threading.Tasks;
using Addressable;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class SceneService : ISceneService
    {
        readonly IAddressableService _addressableService;

        public SceneService(IAddressableService addressableService)
        {
            _addressableService = addressableService;
        }

        public async Task SwitchSceneAsync(ISceneDefinition sceneDefinitionToUnload,
            ISceneDefinition sceneDefinitionToLoad, LoadSceneMode loadSceneMode)
        {
            await LoadSceneAsync(sceneDefinitionToLoad, loadSceneMode);
            await UnloadSceneAsync(sceneDefinitionToUnload);
        }

        public Task LoadSceneAsync(ISceneDefinition sceneDefinitionToLoad, LoadSceneMode loadSceneMode)
        {
            return _addressableService.LoadSceneAsync(sceneDefinitionToLoad.AssetReference, loadSceneMode);
        }

        public Task UnloadSceneAsync(ISceneDefinition sceneDefinitionToUnload)
        {
            return _addressableService.UnloadSceneAsync(sceneDefinitionToUnload.AssetReference);
        }
    }
}
