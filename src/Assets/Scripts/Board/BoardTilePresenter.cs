using DG.Tweening;
using UnityEngine;
using VContainer;

namespace Board
{
    public class BoardTilePresenter
    {
        Vector2Int _position;
        readonly BoardTileView _boardTileView;
        readonly IBoardTileDefinition _boardTileDefinition;

        [Inject]
        readonly BoardSettings _boardSettings;

        [Inject]
        readonly IBoardScenePresenter _boardScenePresenter;

        public string Id => _boardTileDefinition.Name;
        public int GrantedPoints => _boardTileDefinition.GrantedPoints;
        public Vector2Int Position => _position;
        public GameObject GameObject => _boardTileView.gameObject;

        public bool IsConnected { get; set; }

        public BoardTilePresenter(IBoardTileDefinition boardTileDefinition, BoardTileView boardTileView)
        {
            _boardTileDefinition = boardTileDefinition;
            _boardTileView = boardTileView;
            _boardTileView.OnClickCallback = OnClickCallback;

            IsConnected = false;
        }

        public void SetPosition(Vector2Int position)
        {
            _position = position;

            GameObject.transform.position = new Vector2(_position.x, _position.y + 1);
            GameObject.transform.DOMoveY(_position.y, _boardSettings.BoardTileSetPositionDuration);

            GameObject.name = $"BoardTile_({_position.x},{_position.y})";
        }

        public void SetAsCollected()
        {
            _boardTileView.SetAsCollected();
        }

        void OnClickCallback()
        {
            Debug.Log($"<color=white>Clicked on board tile '{GameObject.name}' with ID '{Id}'</color>");

            _boardScenePresenter.FindConnectedBoardTiles(this);
        }
    }
}
