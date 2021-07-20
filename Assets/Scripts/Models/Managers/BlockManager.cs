using Dataers;
using Libs;
using UnityEngine;
using View;

namespace Models.Managers
{
    public class BlockManager : MonoBehaviour
    {
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
        private BlockTypeManager blockTypeManager;
        

        public int height;
        public int width;
        public Block[,] Blocks;
        public BlockType[,] Types;

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
                    
                    TouchBlock(i, j, ball.damage);
                    return;
                }
            }
        }

        private void MakeNewGrid(int[] data)
        {
            Blocks = new Block[height, width];
            Types = new BlockType[height, width];
            _emptyBlocks = 0;
            _allBlocks = width * height;

            var grid = gridOfObjects.NewGrid(height, width);
            var cellSize = gridOfObjects.GetCellSize();
            
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    Types[i, j] = (BlockType)data[i * width + width - 1 - j];

                    switch (Types[i, j])
                    {
                        case BlockType.Empty:
                            _allBlocks--;
                            continue;
                        case BlockType.Unbreakable:
                            _allBlocks--;
                            break;
                    }

                    var typeData = blockTypeManager.GetData(Types[i, j]);

                    Blocks[i, j] = spawnManager.GetBlock();
                    Blocks[i, j].Init(spawnManager, typeData.hitPoint, typeData.endAction);
                    Blocks[i, j].id = i * width + j;
                    Blocks[i, j].type = Types[i, j];
                    
                    Blocks[i, j].blockView.Init(grid[i, j].x, grid[i, j].y, cellSize.x, cellSize.y, mainCamera);
                    Blocks[i, j].blockView.SetColor(typeData.color);
                    
                    Blocks[i, j].transform.SetParent(transform);
                }
            }
        }

        private void TouchBlock(int i, int j, int damage)
        {
            if (Blocks[i, j] == null || !Blocks[i, j].isActiveAndEnabled) return;

            Blocks[i, j].Touch(damage);
            Blocks[i, j].blockView.Touch();
        }

        public void TouchBlock(int i, int j)
        {
            if (Blocks[i, j] == null || !Blocks[i, j].isActiveAndEnabled) return;

            Blocks[i, j].Remove();
            Blocks[i, j].blockView.Touch();
        }

        private void ClearAllBlocks()
        {
            if (Blocks == null) return;
            
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (Blocks[i, j] == null || !Blocks[i, j].isActiveAndEnabled) continue;

                    Blocks[i, j].Remove();
                }
            }
        }

        public void PopBlock(Block block)
        {
            _emptyBlocks++;
            progressbar.SetProgress((float) _emptyBlocks / _allBlocks);

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
