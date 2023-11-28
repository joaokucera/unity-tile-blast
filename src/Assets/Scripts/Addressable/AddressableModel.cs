using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Addressable
{
    public class AddressableModel
    {
        public readonly Dictionary<object, Object> LoadedAssetsByAssetKey = new();
        public readonly Dictionary<object, SceneInstance> SceneInstancesByAssetKey = new();
    }
}
