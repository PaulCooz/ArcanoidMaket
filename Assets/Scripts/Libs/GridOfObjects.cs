using UnityEngine;

namespace Libs
{
    public class GridOfObjects : MonoBehaviour
    {
        private int _height;
        private int _width;
        private Vector2 _cellSize;
        
        [SerializeField] [Range(0, 1)]
        private float height = 0.90f;
        [SerializeField] [Range(0, 1)]
        private float width = 1.0f;
        [SerializeField] [Range(0, 1)]
        private float maxHeight = 0.50f;
        [SerializeField] [Range(0, 1)]
        private float maxWidth = 1.00f;
        [SerializeField] [Range(0, 1)]
        private float spaceByHeight = 0.01f;
        [SerializeField] [Range(0, 1)]
        private float spaceByWidth = 0.01f;

        public Vector2[,] NewGrid(int gridHeight, int gridWidth)
        {
            _height = gridHeight;
            _width = gridWidth;
            _cellSize = GetCellSize();

            var grid = new Vector2[_height, _width];
            for (var i = 0; i < _height; i++)
            {
                for (var j = 0; j < _width; j++)
                {
                    grid[i, j] = GetCellPosition(i, j);
                }
            }

            return grid;
        }

        public Vector2 GetCellSize()
        {
            return new Vector2
            {
                x = (maxWidth - spaceByWidth * (_width + 1)) / _width,
                y = (maxHeight - spaceByHeight * (_height + 1)) / _height
            };
        }

        private Vector2 GetCellPosition(int i, int j)
        {
            return new Vector2
            {
                x = width - (spaceByWidth * (j + 1) + j * _cellSize.x + _cellSize.x / 2),
                y = height - (spaceByHeight * (i + 1) + i * _cellSize.y + _cellSize.y / 2)
            };
        }
    }
}