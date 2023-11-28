using System.Threading.Tasks;

namespace Board
{
    public interface IBoardScenePresenter
    {
        Task FindConnectedBoardTiles(BoardTilePresenter boardTilePresenter);
    }
}
