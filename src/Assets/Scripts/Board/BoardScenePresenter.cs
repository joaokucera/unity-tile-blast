using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pool;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Board
{
    public class BoardScenePresenter : IStartable, IBoardScenePresenter, IDisposable
    {
        readonly BoardModel _boardModel;
        readonly BoardSceneView _boardSceneView;
        readonly BoardSettings _boardSettings;
        readonly BoardCatalogue _boardCatalogue;
        readonly IBoardFactory _boardFactory;
        readonly IPoolService _poolService;

        bool _isFindConnectedBoardTilesRunning;

        [Inject]
        public BoardScenePresenter(BoardModel boardModel, BoardSceneView boardSceneView, BoardSettings boardSettings,
            BoardCatalogue boardCatalogue, IBoardFactory boardFactory, IPoolService poolService)
        {
            _boardModel = boardModel;
            _boardSceneView = boardSceneView;
            _boardSettings = boardSettings;
            _boardCatalogue = boardCatalogue;
            _boardFactory = boardFactory;
            _poolService = poolService;
        }

        public async void Start()
        {
            _boardSceneView.ScoreText.text = _boardModel.CurrentScore.ToString();

            int width = _boardSettings.Width;
            int height = _boardSettings.Height;

            BoardHelper.SetBoardCameraPosition(_boardSceneView.BoardCamera, width, height);

            await CreateNewBoard(width, height);
        }

        async Task CreateNewBoard(int width, int height)
        {
            _boardModel.BoardVO = new BoardVO(width, height);

            await AddNewBoardTiles(width, height, true);
        }

        async Task AddNewBoardTiles(int width, int height, bool isNewBoard)
        {
            var boardTilePresenters = _boardModel.BoardVO.BoardTilePresenters;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (boardTilePresenters[x, y] == null)
                    {
                        await AddNewBoardTile(x, y, isNewBoard);
                    }
                }
            }
        }

        async Task AddNewBoardTile(int x, int y, bool isNewBoard)
        {
            var poolDefinition = isNewBoard
                ? _boardCatalogue.GetBoardTilePoolDefinition(
                    _boardSettings.InitialBoardLayout.Rows[x].Columns[y].TileValue)
                : _boardCatalogue.GetRandomBoardTilePoolDefinition();

            var poolInstance = await _poolService.GetPooledObjectAsync(poolDefinition, _boardSceneView.BoardParent);

            var position = new Vector2Int(x, y);
            var boardTilePresenter = _boardFactory.CreateBoardTilePresenter(poolDefinition, position, poolInstance);

            _boardModel.BoardVO.AddBoardTile(position, boardTilePresenter);
        }

        public async Task FindConnectedBoardTiles(BoardTilePresenter boardTilePresenter)
        {
            const int minConnectedBoardTiles = 2;

            if (_isFindConnectedBoardTilesRunning)
            {
                Debug.Log("<color=magenta>Find connected board tiles is already running</color>");
                return;
            }

            _isFindConnectedBoardTilesRunning = true;

            int width = _boardSettings.Width;
            int height = _boardSettings.Height;

            var connectedBoardTiles = GetConnectedBoardTiles(width, height, boardTilePresenter);
            if (connectedBoardTiles.Count < minConnectedBoardTiles)
            {
                Debug.Log("<color=magenta>Not enough connected board tiles to collect</color>");
            }
            else
            {
                await CollectConnectedBoardTiles(connectedBoardTiles);
                await AddScore(connectedBoardTiles);
                await RearrangeBoardTiles(width, height);
                await AddNewBoardTiles(width, height, false);
            }

            _isFindConnectedBoardTilesRunning = false;
        }

        List<BoardTilePresenter> GetConnectedBoardTiles(int width, int height, BoardTilePresenter boardTilePresenter)
        {
            BoardHelper.ConnectedBoardTiles.Clear();

            string id = boardTilePresenter.Id;
            var position = boardTilePresenter.Position;

            GetConnectedBoardTiles(width, height, id, position.x, position.y, BoardHelper.ConnectedBoardTiles);
            ResetBoardTilesState();

            return BoardHelper.ConnectedBoardTiles;
        }

        void GetConnectedBoardTiles(int width, int height, string id, int x, int y,
            ICollection<BoardTilePresenter> connectedBoardTiles)
        {
            if (!BoardHelper.IsBoardTileInBounds(width, height, x, y))
            {
                return;
            }

            var boardTilePresenters = _boardModel.BoardVO.BoardTilePresenters;
            if (boardTilePresenters[x, y].Id != id || boardTilePresenters[x, y].IsConnected)
            {
                return;
            }

            boardTilePresenters[x, y].IsConnected = true;
            connectedBoardTiles.Add(boardTilePresenters[x, y]);

            GetConnectedBoardTiles(width, height, id, x + 1, y, connectedBoardTiles);
            GetConnectedBoardTiles(width, height, id, x - 1, y, connectedBoardTiles);
            GetConnectedBoardTiles(width, height, id, x, y + 1, connectedBoardTiles);
            GetConnectedBoardTiles(width, height, id, x, y - 1, connectedBoardTiles);
        }

        void ResetBoardTilesState()
        {
            foreach (var boardTilePresenter in _boardModel.BoardVO.BoardTilePresenters)
            {
                boardTilePresenter.IsConnected = false;
            }
        }

        async Task CollectConnectedBoardTiles(IEnumerable<BoardTilePresenter> connectedBoardTiles)
        {
            foreach (var boardTilePresenter in connectedBoardTiles)
            {
                boardTilePresenter.SetAsCollected();
            }

            await Task.Delay(TimeSpan.FromSeconds(_boardSettings.CollectConnectBoardTilesDelayTime));

            foreach (var boardTilePresenter in connectedBoardTiles)
            {
                _poolService.ReturnPooledObject(boardTilePresenter.GameObject);
                _boardModel.BoardVO.BoardTilePresenters[boardTilePresenter.Position.x, boardTilePresenter.Position.y] =
                    null;
            }
        }

        async Task AddScore(IEnumerable<BoardTilePresenter> connectedBoardTiles)
        {
            foreach (var connectedBoardTile in connectedBoardTiles)
            {
                _boardModel.CurrentScore += connectedBoardTile.GrantedPoints;

                await Task.Delay(TimeSpan.FromSeconds(_boardSettings.ScorePointsDelayTime));

                _boardSceneView.ScoreText.text = _boardModel.CurrentScore.ToString();
            }
        }

        async Task RearrangeBoardTiles(int width, int height)
        {
            var boardTilePresenters = _boardModel.BoardVO.BoardTilePresenters;

            for (int x = 0; x < width; x++)
            {
                int emptySpaces = 0;

                for (int y = 0; y < height; y++)
                {
                    if (boardTilePresenters[x, y] == null)
                    {
                        emptySpaces++;
                    }
                    else if (emptySpaces > 0)
                    {
                        var newPosition = new Vector2Int(x, y - emptySpaces);

                        boardTilePresenters[newPosition.x, newPosition.y] = boardTilePresenters[x, y];
                        boardTilePresenters[newPosition.x, newPosition.y].SetPosition(newPosition);
                        boardTilePresenters[x, y] = null;
                    }
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(_boardSettings.BoardTileSetPositionDuration));
        }

        public void Dispose()
        {
            _poolService.ReturnAllPooledChildren(_boardSceneView.BoardParent);
            _boardModel.BoardVO = null;
        }
    }
}
