using System;
using Libs.Interfaces;
using Models.Managers;
using ScriptObjects;
using UnityEngine;

namespace Libs
{
    public class GridOfObjects<T> : MonoBehaviour where T : MonoBehaviour
    {
        private int _height;
        private int _width;
        protected Vector2 _cellSize;

        [SerializeField]
        protected SpawnManager spawnManager;
        [SerializeField]
        protected Camera mainCamera;
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
        [SerializeField] 
        private GameConfig config;

        public T[,] NewGrid(int gridHeight, int gridWidth, string[] tags)
        {
            _height = gridHeight;
            _width = gridWidth;
            _cellSize = GetCellSize();

            var grid = new T[_height, _width];
            for (var i = 0; i < _height; i++)
            {
                for (var j = 0; j < _width; j++)
                {
                    if (tags[i * _width + j] == config.EmptyCellName) continue;
                    
                    grid[i, j] = CreateCell(grid[i, j], i, j, tags[i * _width + j]);
                }
            }

            return grid;
        }

        protected virtual T CreateCell(T monoBehaviour, int i, int i1, string s)
        {
            Debug.LogWarning("can't create cell (not overridden)");
            throw new NotImplementedException();
        }

        private Vector2 GetCellSize()
        {
            return new Vector2
            {
                x = (maxWidth - spaceByWidth * (_width + 1)) / _width,
                y = (maxHeight - spaceByHeight * (_height + 1)) / _height
            };
        }

        protected Vector2 GetCellPosition(int i, int j)
        {
            return new Vector2
            {
                x = width - (spaceByWidth * (j + 1) + j * _cellSize.x + _cellSize.x / 2),
                y = height - (spaceByHeight * (i + 1) + i * _cellSize.y + _cellSize.y / 2)
            };
        }
    }
}