using System;
using UnityEngine;

namespace Board
{
    [Serializable]
    public class BoardLayout
    {
        [SerializeField]
        BoardLayoutRow[] rows;

        public BoardLayoutRow[] Rows => rows;

        public BoardLayout(int rowsCount, int columnCount, int maxTileValue)
        {
            rows = new BoardLayoutRow[rowsCount];

            for (int i = 0; i < rowsCount; i++)
            {
                rows[i] = new BoardLayoutRow(columnCount,maxTileValue);
            }
        }
    }
}
