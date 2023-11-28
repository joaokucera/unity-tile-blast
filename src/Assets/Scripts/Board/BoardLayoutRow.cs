using System;
using UnityEngine;

namespace Board
{
    [Serializable]
    public class BoardLayoutRow
    {
        [SerializeReference]
        BoardLayoutColumn[] columns;

        public BoardLayoutColumn[] Columns => columns;

        public BoardLayoutRow(int columnsCount, int maxTileValue)
        {
            columns = new BoardLayoutColumn[columnsCount];

            for (int i = 0; i < columnsCount; i++)
            {
                columns[i] = new BoardLayoutColumn(maxTileValue);
            }
        }
    }
}
