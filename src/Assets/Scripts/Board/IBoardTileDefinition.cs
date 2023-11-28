using UnityEngine.AddressableAssets;

namespace Board
{
    public interface IBoardTileDefinition
    {
        string Name { get; }
        int GrantedPoints { get; }
        AssetReference AssetReference { get; }
    }
}
