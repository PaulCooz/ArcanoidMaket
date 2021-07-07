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
            var collisionBlockId = collision.gameObject.GetComponent<Block>()?.id;
                
            if (collisionBlockId == null) return;

            for (var i = 0; i < _height; i++)
            {
                for (var j = 0; j < _width; j++)
                {
                    if (_blockTypes[i, j] == BlockTypes.Unbreakable || i * _width + j != collisionBlockId) continue;
                    
                    _blocks[i, j].Remove();
                    break;
                }
            }
        }

        private void MakeNewGrid(int[] data)
        {
            ClearOldBlocks();
            
            _blocks = new Block[_height, _width];
            _blockTypes = new BlockTypes[_height, _width];
            _emptyBlocks = 0;
            
            var grid = gridOfObjects.NewGrid(_height, _width);
            for (var i = 0; i < _height; i++)
            {
                for (var j = 0; j < _width; j++)
                {
                    if (i * _width + j == 5 || i * _width + j == 8)
                    {
                        print("spawn unb");
                    }
                    
                    _blocks[i, j] = spawnManager.GetBlock();
                    _blocks[i, j].Init(grid[i, j].x, grid[i, j].y, grid[i, j].z, grid[i, j].w, 
                                       spawnManager, mainCamera);
                    _blocks[i, j].transform.SetParent(transform);
                    _blocks[i, j].id = i * _width + j;
                    _blocks[i, j].OnDeactivate += PopBlock;
                    
                    _blockTypes[i, j] = (BlockTypes)data[i * _width + j];

                    if (_blockTypes[i, j] == BlockTypes.Unbreakable) _emptyBlocks++;
                    
                    MakeTypical(_blocks[i, j], _blockTypes[i, j]);
                }
            }
        }

        private void ClearOldBlocks()
        {
            if (_blocks == null) return;
            
            for (var i = 0; i < _height; i++)
            {
                for (var j = 0; j < _width; j++)
                {
                    if (!_blocks[i, j].isActiveAndEnabled) continue;

                    _blocks[i, j].Remove(true);
                }
            }
        }

        private void MakeTypical(Block block, BlockTypes blockType)
        {
            switch (blockType)
            {
            case BlockTypes.Empty:
                block.Remove();
                break;
            case BlockTypes.Common:
                block.SetColor(Color.red);
                break;
            case BlockTypes.Unbreakable:
                block.SetColor(Color.grey);
                break;
            case BlockTypes.Bomb:
                block.SetColor(Color.magenta);
                break;
            }
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
