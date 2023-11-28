using Scene;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Boot
{
    public class BootScenePresenter : IStartable
    {
        readonly ISceneService _sceneService;
        readonly SceneCatalogue _sceneCatalogue;

        public BootScenePresenter(ISceneService sceneService, SceneCatalogue sceneCatalogue)
        {
            _sceneService = sceneService;
            _sceneCatalogue = sceneCatalogue;
        }

        public async void Start()
        {
            await _sceneService.LoadSceneAsync(_sceneCatalogue.MenuSceneDefinition, LoadSceneMode.Additive);
        }
    }
}
