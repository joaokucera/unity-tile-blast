using Scene;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Menu
{
    public class MenuScenePresenter : IStartable
    {
        readonly MenuSceneView _menuSceneView;
        readonly ISceneService _sceneService;
        readonly SceneCatalogue _sceneCatalogue;

        public MenuScenePresenter(MenuSceneView menuSceneView, ISceneService sceneService,
            SceneCatalogue sceneCatalogue)
        {
            _menuSceneView = menuSceneView;
            _sceneService = sceneService;
            _sceneCatalogue = sceneCatalogue;
        }

        public void Start()
        {
            _menuSceneView.PlayButton.onClick.AddListener(OnPlayButtonClicked);
        }

        async void OnPlayButtonClicked()
        {
            await _sceneService.SwitchSceneAsync(_sceneCatalogue.MenuSceneDefinition,
                _sceneCatalogue.GameSceneDefinition, LoadSceneMode.Additive);
        }
    }
}
