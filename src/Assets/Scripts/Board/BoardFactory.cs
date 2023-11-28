using UnityEngine;
using VContainer;

namespace Board
{
    public class BoardFactory : IBoardFactory
    {
        readonly IObjectResolver _container;

        public BoardFactory(IObjectResolver container)
        {
            _container = container;
        }

        public BoardTilePresenter CreateBoardTilePresenter(IBoardTileDefinition boardTileDefinition,
            Vector2Int position, GameObject poolInstance)
        {
            var boardTileView = poolInstance.GetComponent<BoardTileView>();
            var boardTilePresenter = new BoardTilePresenter(boardTileDefinition, boardTileView);

            _container.Inject(boardTilePresenter);

            boardTilePresenter.SetPosition(position);
            return boardTilePresenter;
        }
    }
}
