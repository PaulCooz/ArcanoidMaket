using System;
using Libs;
using Loaders;
using Logics.Balls;
using Logics.Spawns;
using UnityEngine;
using View;

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
        private int _allBlocks;
        private int _emptyBlocks;
        
        [SerializeField]
        private GridOfObjects gridOfObjects;
        [SerializeField] 
        private SpawnManager spawnManager;
        [SerializeField] 
        private Camera mainCamera;
        [SerializeField] 
        private Progressbar progressbar;

        private void Awake()
        {
            EventsAndStates.OnGameStart += NewLevel;
        }

        private void NewLevel(LevelData levelData)
        {
            ClearAllBlocks();
            
            _height = levelData.height;
            _width = levelData.width;
            MakeNewGrid(levelData.data);
            
            progressbar.SetProgress(0);
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
                    
                    _blocks[i, j].Touch();
                    _blocks[i, j].blockView.Touch();
                    return;
                }
            }
        }

        private void MakeNewGrid(int[] data)
        {
            _blocks = new Block[_height, _width];
            _blockTypes = new BlockTypes[_height, _width];
            _emptyBlocks = 0;
            _allBlocks = _width * _height;

            var grid = gridOfObjects.NewGrid(_height, _width);
            for (var i = 0; i < _height; i++)
            {
                for (var j = 0; j < _width; j++)
                {
                    _blockTypes[i, j] = (BlockTypes)data[i * _width + j];

                    if (_blockTypes[i, j] == BlockTypes.Empty)
                    {
                        _allBlocks--;
                        continue;
                    }
                    if (_blockTypes[i, j] == BlockTypes.Unbreakable)
                    {
                        _allBlocks--;
                    }

                    _blocks[i, j] = spawnManager.GetBlock();
                    _blocks[i, j].Init(spawnManager);
                    _blocks[i, j].blockView.Init(grid[i, j].x, grid[i, j].y, grid[i, j].z, grid[i, j].w, mainCamera);
                    _blocks[i, j].transform.SetParent(transform);
                    _blocks[i, j].id = i * _width + j;
                    _blocks[i, j].OnDeactivate += PopBlock;

                    MakeTypical(_blocks[i, j], _blockTypes[i, j]);
                }
            }
        }

        private void ClearAllBlocks()
        {
            if (_blocks == null) return;
            
            for (var i = 0; i < _height; i++)
            {
                for (var j = 0; j < _width; j++)
                {
                    if (_blockTypes[i, j] == BlockTypes.Empty || !_blocks[i, j].isActiveAndEnabled) continue;

                    _blocks[i, j].Remove();
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
                block.blockView.SetColor(Color.red);
                break;
            case BlockTypes.Unbreakable:
                block.blockView.SetColor(Color.grey);
                break;
            case BlockTypes.Bomb:
                block.blockView.SetColor(Color.magenta);
                break;
            default:
                Debug.LogWarning("unknown block type");
                break;
            }
        }

        private void PopBlock(int id)
        {
            _emptyBlocks++;
            progressbar.SetProgress((float) _emptyBlocks / _allBlocks);
            
            if (_emptyBlocks == _allBlocks)
            {
                EventsAndStates.SetGameWin();
            }
            else
            {
                int i = id / _width, j = id % _width;
                
                BonusCheck(_blockTypes[i, j], i, j);
            }
        }

        private void BonusCheck(BlockTypes blockType, int i, int j)
        {
            if (blockType == BlockTypes.Bomb)
            {
                
            }
        }

        private void OnDestroy()
        {
            EventsAndStates.OnGameStart -= NewLevel;
        }
    }
}
