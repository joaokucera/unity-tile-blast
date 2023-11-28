using Addressable;
using Pool;
using Scene;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Boot
{
    public class BootSceneLifetimeScope : LifetimeScope
    {
        [SerializeField]
        SceneCatalogue sceneCatalogue;

        protected override void Configure(IContainerBuilder builder)
        {
            // Boot
            builder.Register<BootScenePresenter>(Lifetime.Singleton).AsImplementedInterfaces();

            // Addressable
            builder.Register<AddressableModel>(Lifetime.Singleton);
            builder.Register<IAddressableService, AddressableService>(Lifetime.Singleton);

            // Scene
            builder.RegisterInstance(sceneCatalogue);
            builder.Register<ISceneService, SceneService>(Lifetime.Singleton);

            // Pool
            builder.Register<PoolModel>(Lifetime.Singleton);
            builder.Register<PoolService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<IPoolFactory, PoolFactory>(Lifetime.Singleton);
        }
    }
}
