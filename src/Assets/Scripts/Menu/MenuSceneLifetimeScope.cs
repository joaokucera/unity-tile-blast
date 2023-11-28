using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Menu
{
    [RequireComponent(typeof(MenuSceneView))]
    public class MenuSceneLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Menu
            builder.RegisterComponent(GetComponent<MenuSceneView>());
            builder.Register<MenuScenePresenter>(Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
}
