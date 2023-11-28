using UnityEngine;

namespace Board
{
    public class BoardVO
    {
        public readonly BoardTilePresenter[,] BoardTilePresenters;

        public BoardVO(int width, int height)
        {
            BoardTilePresenters = new BoardTilePresenter[width, height];
        }

        public void AddBoardTile(Vector2Int position, BoardTilePresenter boardTilePresenter)
        {
            BoardTilePresenters[position.x, position.y] = boardTilePresenter;
        }
    }
}
