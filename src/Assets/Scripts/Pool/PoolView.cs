using UnityEngine;

namespace Pool
{
    public class PoolView : MonoBehaviour
    {
        IPoolService _poolService;
        string _assetName;

        public void Init(IPoolService poolService, string assetName)
        {
            _poolService = poolService;
            _assetName = assetName;
        }

        public void ReturnToPool()
        {
            _poolService.ReturnPooledObject(_assetName, gameObject);
        }
    }
}
