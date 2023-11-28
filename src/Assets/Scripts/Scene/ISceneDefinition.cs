using UnityEngine.AddressableAssets;

namespace Scene
{
    public interface ISceneDefinition
    {
        string Name { get; }
        AssetReference AssetReference { get; }
    }
}
