using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public static class BoardHelper
    {
        public static readonly List<BoardTilePresenter> ConnectedBoardTiles = new();

        public static void SetBoardCameraPosition(Camera boardCamera, int width, int height)
        {
            var boardCameraPosition = boardCamera.transform.position;

            boardCameraPosition.x = width % 2f == 0
                ? width / 2f - 0.5f
                : (int)(width / 2f);

            boardCameraPosition.y = height % 2f == 0
                ? height / 2f + 0.5f
                : (int)(height / 2f);

            boardCamera.transform.position = boardCameraPosition;
        }

        public static bool IsBoardTileInBounds(int width, int height, int x, int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }
    }
}
