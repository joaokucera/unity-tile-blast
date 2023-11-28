using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Board
{
    [Serializable]
    public class BoardLayoutColumn
    {
        [SerializeField]
        int tileValue;

        public int TileValue
        {
            get { return tileValue; }
#if UNITY_EDITOR
            set { tileValue = value; }
#endif
        }

        public BoardLayoutColumn(int maxTileValue)
        {
            tileValue = Random.Range(0, maxTileValue);
        }
    }
}
