using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Board
{
    [Serializable]
    public class BoardTileSettings
    {
        [SerializeField]
        int grantedPoint;

        [SerializeField]
        AssetReference assetReference;

        public int GrantedPoint => grantedPoint;
        public AssetReference AssetReference => assetReference;
    }
}
