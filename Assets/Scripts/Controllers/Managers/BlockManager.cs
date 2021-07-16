using Libs;
using Loaders;
using Logics;
using Logics.Spawns;
using UnityEngine;
using View;

namespace Controllers.Managers
{
    public enum BlockTypes
    {
        Empty,
        Common,
        Unbreakable,
        Bomb,
        ChainBomb
    }
    
    public class BlockManager : MonoBehaviour
    {
        public int height;
        public int width;
        public Block[,] Blocks;
        public BlockTypes[,] Types;
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
        [SerializeField] 
        private BonusManager bonusManager;

        private void Awake()
        {
            EventsAndStates.OnGameStart += NewLevel;
        }

        private void NewLevel(LevelData levelData)
        {
            ClearAllBlocks();
            
            height = levelData.height;
            width = levelData.width;
            MakeNewGrid(levelData.data);
            
            progressbar.SetProgress(0);
        }

        public void SomeBlockTouched(Ball ball, Collision2D collision)
        {
            var collisionBlockId = collision.gameObject.GetComponent<Block>()?.id;
                
            if (collisionBlockId == null) return;

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (i * width + j != collisionBlockId) continue;

                    TouchBlock(i, j, 1);
                    return;
                }
            }
        }

        private void MakeNewGrid(int[] data)
        {
            Blocks = new Block[height, width];
            Types = new BlockTypes[height, width];
            _emptyBlocks = 0;
            _allBlocks = width * height;

            var grid = gridOfObjects.NewGrid(height, width);
            var cellSize = gridOfObjects.GetCellSize();
            
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    Types[i, j] = (BlockTypes)data[i * width + j];

                    if (Types[i, j] == BlockTypes.Empty)
                    {
                        _allBlocks--;
                        continue;
                    }
                    if (Types[i, j] == BlockTypes.Unbreakable)
                    {
                        _allBlocks--;
                    }

                    Blocks[i, j] = spawnManager.GetBlock();
                    Blocks[i, j].Init(spawnManager);
                    Blocks[i, j].blockView.Init(grid[i, j].x, grid[i, j].y, cellSize.x, cellSize.y, mainCamera);
                    Blocks[i, j].transform.SetParent(transform);
                    Blocks[i, j].id = i * width + j;
                    
                    bonusManager.MakeTypical(Blocks[i, j], Types[i, j]);
                }
            }
        }

        public void TouchBlock(int i, int j, int damage)
        {
            if (Blocks[i, j] == null || !Blocks[i, j].isActiveAndEnabled || Types[i, j] == BlockTypes.Unbreakable) return;

            Blocks[i, j].Touch(damage);
            Blocks[i, j].blockView.Touch();
            
            if (!Blocks[i, j].isActiveAndEnabled)
            {
                PopBlock(Blocks[i, j].id);
            }
        }

        private void ClearAllBlocks()
        {
            if (Blocks == null) return;
            
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (Types[i, j] == BlockTypes.Empty || !Blocks[i, j].isActiveAndEnabled) continue;

                    Blocks[i, j].Remove();
                }
            }
        }

        private void PopBlock(int id)
        {
            int i = id / width, j = id % width;

            if (Types[i, j] == BlockTypes.Unbreakable) return;

            _emptyBlocks++;
            progressbar.SetProgress((float) _emptyBlocks / _allBlocks);
            bonusManager.BonusCheck(Types[i, j], i, j);
            
            if (_emptyBlocks == _allBlocks)
            {
                EventsAndStates.SetGameWin();
            }
        }

        private void OnDestroy()
        {
            EventsAndStates.OnGameStart -= NewLevel;
        }
    }
}
