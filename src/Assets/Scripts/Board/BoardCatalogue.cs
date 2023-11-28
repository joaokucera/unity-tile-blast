using UnityEngine;
using VContainer.Unity;

namespace Board
{
    [CreateAssetMenu(fileName = "BoardCatalogue", menuName = "Board/BoardCatalogue")]
    public class BoardCatalogue : ScriptableObject, IInitializable
    {
        [SerializeField]
        BoardTileSettings[] boardTileSettingsArray;

        IBoardTileDefinition[] _boardTilePoolDefinitions;

#if UNITY_EDITOR
        public BoardTileSettings[] BoardTileSettingsArray => boardTileSettingsArray;
#endif

        public void Initialize()
        {
            _boardTilePoolDefinitions = new IBoardTileDefinition[boardTileSettingsArray.Length];

            for (int i = 0; i < boardTileSettingsArray.Length; i++)
            {
                _boardTilePoolDefinitions[i] = new BoardTileDefinition($"BoardTile{i}",
                    boardTileSettingsArray[i].GrantedPoint, boardTileSettingsArray[i].AssetReference);
            }
        }

        public IBoardTileDefinition GetBoardTilePoolDefinition(int index)
        {
            if (index >= 0 && index < _boardTilePoolDefinitions.Length)
            {
                return _boardTilePoolDefinitions[index];
            }

            Debug.Log($"Index provided is out of range: {index}. Will return first board tile pool definition.");
            return _boardTilePoolDefinitions[0];
        }

        public IBoardTileDefinition GetRandomBoardTilePoolDefinition()
        {
            int randomIndex = Random.Range(0, _boardTilePoolDefinitions.Length);

            return GetBoardTilePoolDefinition(randomIndex);
        }
    }
}
