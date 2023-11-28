using Pool;
using UnityEngine;

namespace Board
{
    public interface IBoardFactory
    {
        BoardTilePresenter CreateBoardTilePresenter(IBoardTileDefinition boardTileDefinition, Vector2Int position,
            GameObject poolInstance);
    }
}
