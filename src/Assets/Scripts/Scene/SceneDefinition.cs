using UnityEngine.AddressableAssets;

namespace Scene
{
    public class SceneDefinition : ISceneDefinition
    {
        public string Name { get; }
        public AssetReference AssetReference { get; }

        public SceneDefinition(string name, AssetReference assetReference)
        {
            Name = name;
            AssetReference = assetReference;
        }
    }
}
