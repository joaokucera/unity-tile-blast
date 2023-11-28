using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Scene
{
    [CreateAssetMenu(fileName = "SceneCatalogue", menuName = "Scene/SceneCatalogue")]
    public class SceneCatalogue : ScriptableObject
    {
        [SerializeField]
        AssetReference menuSceneAssetReference;

        [SerializeField]
        AssetReference gameSceneAssetReference;

        ISceneDefinition _menuSceneDefinition;
        ISceneDefinition _gameSceneDefinition;

        public ISceneDefinition MenuSceneDefinition =>
            _menuSceneDefinition ??= new SceneDefinition("MenuScene", menuSceneAssetReference);

        public ISceneDefinition GameSceneDefinition =>
            _gameSceneDefinition ??= new SceneDefinition("GameScene", gameSceneAssetReference);
    }
}
