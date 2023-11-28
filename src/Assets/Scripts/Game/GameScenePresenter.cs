using System;
using System.Collections.Generic;
using Board;
using Scene;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Game
{
    public class GameScenePresenter : IStartable
    {
        readonly GameSceneView _gameSceneView;
        readonly ISceneService _sceneService;
        readonly SceneCatalogue _sceneCatalogue;

        public GameScenePresenter(GameSceneView gameSceneView, ISceneService sceneService,
            SceneCatalogue sceneCatalogue)
        {
            _gameSceneView = gameSceneView;
            _sceneService = sceneService;
            _sceneCatalogue = sceneCatalogue;
        }

        public void Start()
        {
            _gameSceneView.ReturnButton.onClick.AddListener(OnPlayButtonClicked);
        }

        async void OnPlayButtonClicked()
        {
            await _sceneService.SwitchSceneAsync(_sceneCatalogue.GameSceneDefinition,
                _sceneCatalogue.MenuSceneDefinition, LoadSceneMode.Additive);
        }
    }
}
