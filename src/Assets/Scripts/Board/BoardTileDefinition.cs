using UnityEngine.AddressableAssets;

namespace Board
{
    public class BoardTileDefinition : IBoardTileDefinition
    {
        public string Name { get; }

        public int GrantedPoints { get; }

        public AssetReference AssetReference { get; }

        public BoardTileDefinition(string name, int grantedPoints, AssetReference assetReference)
        {
            Name = name;
            GrantedPoints = grantedPoints;
            AssetReference = assetReference;
        }
    }
}
