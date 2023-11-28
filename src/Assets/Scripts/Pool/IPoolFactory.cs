using UnityEngine;

namespace Pool
{
    public interface IPoolFactory
    {
        PoolView CreatePoolView(GameObject prefab);
    }
}
