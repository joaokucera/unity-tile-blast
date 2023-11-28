using System.Threading.Tasks;
using Board;
using UnityEngine;

namespace Pool
{
    public interface IPoolService
    {
        Task<GameObject> GetPooledObjectAsync(IBoardTileDefinition boardTileDefinition, Transform parent = null);

        void ReturnPooledObject(GameObject objectToReturn);

        void ReturnPooledObject(string assetName, GameObject objectToReturn);

        void ReturnAllPooledChildren(Transform parent);
    }
}
