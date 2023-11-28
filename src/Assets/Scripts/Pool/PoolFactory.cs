using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Pool
{
    public class PoolFactory : IPoolFactory
    {
        readonly IObjectResolver _container;

        public PoolFactory(IObjectResolver container)
        {
            _container = container;
        }

        public PoolView CreatePoolView(GameObject prefab)
        {
            var gameObject = _container.Instantiate(prefab);

            return gameObject.AddComponent<PoolView>();
        }
    }
}
