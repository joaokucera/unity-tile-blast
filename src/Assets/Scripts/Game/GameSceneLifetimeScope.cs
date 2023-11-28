using Board;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game
{
    [RequireComponent(typeof(GameSceneView))]
    [RequireComponent(typeof(BoardSceneView))]
    public class GameSceneLifetimeScope : LifetimeScope
    {
        [SerializeField]
        BoardCatalogue boardCatalogue;

        [SerializeField]
        BoardSettings boardSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            // Game
            builder.RegisterComponent(GetComponent<GameSceneView>());
            builder.Register<GameScenePresenter>(Lifetime.Scoped).AsImplementedInterfaces();

            // Board
            builder.RegisterInstance(boardCatalogue).AsImplementedInterfaces();
            builder.RegisterInstance(boardSettings);
            builder.RegisterComponent(GetComponent<BoardSceneView>());
            builder.Register<BoardScenePresenter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<IBoardFactory, BoardFactory>(Lifetime.Scoped);
            builder.Register<BoardModel>(Lifetime.Scoped);
        }
    }
}
