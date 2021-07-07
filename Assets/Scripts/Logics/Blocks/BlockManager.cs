using System.Collections.Generic;
using Libs;
using Logics.Balls;
using Logics.Loaders;
using Logics.Spawns;
using UnityEngine;

namespace Logics.Blocks
{
    public enum BlockTypes
    {
        Empty,
        Common,
        Unbreakable,
        Bomb
    }
    
    public class BlockManager : MonoBehaviour
    {
        private int _height;
        private int _width;
        private Block[,] _blocks;
        private BlockTypes[,] _blockTypes;
        private int _emptyBlocks;
    
        [SerializeField]
        private GridOfObjects gridOfObjects;
        [SerializeField] 
        private LevelManager levelManager;
        [SerializeField] 
        private SpawnManager spawnManager;
        [SerializeField] 
        private Camera mainCamera;

        public void NewLevel(LevelData levelData)
        {
            _height = levelData.height;
            _width = levelData.width;

            MakeNewGrid(levelData.data);
        }

        public void SomeBlockTouched(Ball ball, Collision2D collision)
        {
            for (var i = 0; i < _height; i++)
            {
                for (var j = 0; j < _width; j++)
                {
                    if (_blocks[i, j].gameObject != collision.gameObject) continue;
                    
                    _blocks[i, j].Remove();
                }
            }
        }

        private void MakeNewGrid(int[] data)
        {
            _blocks = new Block[_height, _width];
            _blockTypes = new BlockTypes[_height, _width];
            
            var grid = gridOfObjects.NewGrid(_height, _width);
            for (var i = 0; i < _height; i++)
            {
                for (var j = 0; j < _width; j++)
                {
                    _blockTypes[i, j] = (BlockTypes)data[i * _width + j];
                    
                    _blocks[i, j] = spawnManager.GetBlock();
                    _blocks[i, j].Init(grid[i, j].x, grid[i, j].y, grid[i, j].z, grid[i, j].w, spawnManager, mainCamera);
                    _blocks[i, j].transform.SetParent(transform);
                    _blocks[i, j].OnDeactivate += PopBlock;
                }
            }
            _emptyBlocks = 0;
        }

        private void PopBlock()
        {
            _emptyBlocks++;
            if (_emptyBlocks == _width * _height)
            {
                levelManager.LoadNextLevel();
            }
        }
    }
}
