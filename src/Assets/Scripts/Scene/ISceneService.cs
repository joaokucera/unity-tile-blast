using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Scene
{
    public interface ISceneService
    {
        Task SwitchSceneAsync(ISceneDefinition sceneDefinitionToUnload, ISceneDefinition sceneDefinitionToLoad,
            LoadSceneMode loadSceneMode);

        Task LoadSceneAsync(ISceneDefinition sceneDefinitionToLoad, LoadSceneMode loadSceneMode);

        Task UnloadSceneAsync(ISceneDefinition sceneDefinitionToUnload);
    }
}
