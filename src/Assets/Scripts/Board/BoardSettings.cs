using UnityEngine;

namespace Board
{
    [CreateAssetMenu(fileName = "BoardSettings", menuName = "Board/BoardSettings")]
    public class BoardSettings : ScriptableObject
    {
        [SerializeField, Range(3, 5)]
        int width;

        [SerializeField, Range(5, 7)]
        int height;

        [SerializeField, Range(0.1f, 1f)]
        float collectConnectBoardTilesDelayTime;

        [SerializeField, Range(0.1f, 1f)]
        float scorePointsDelayTime;

        [SerializeField, Range(0.1f, 1f)]
        float boardTileSetPositionDuration;

        [SerializeField]
        BoardLayout initialBoardLayout;

        public int Width => width;
        public int Height => height;
        public float CollectConnectBoardTilesDelayTime => collectConnectBoardTilesDelayTime;
        public float ScorePointsDelayTime => scorePointsDelayTime;
        public float BoardTileSetPositionDuration => boardTileSetPositionDuration;

        public BoardLayout InitialBoardLayout
        {
            get { return initialBoardLayout; }
#if UNITY_EDITOR
            set { initialBoardLayout = value; }
#endif
        }
    }
}
