using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class PoolModel
    {
        public readonly Dictionary<string, Stack<GameObject>> PoolsByAssetName = new();
    }
}
