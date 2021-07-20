using System.Collections.Generic;
using UnityEngine;

namespace Models.Managers
{
    public class BonusManager : MonoBehaviour
    {
        [SerializeField]
        private BlockManager blockManager;
        [SerializeField] 
        private BlockTypeManager blockTypeManager;
        [SerializeField]
        private SpawnManager spawnManager;
        
        public void Bomb(Block block)
        {
            var i = block.id / blockManager.width;
            var j = block.id % blockManager.width;
            
            for (var x = -1; x <= 1; x++)
            for (var y = -1; y <= 1; y++)
            {
                if (Mathf.Abs(x) + Mathf.Abs(y) > 1 || Mathf.Abs(x) + Mathf.Abs(y) == 0) continue;

                var ni = i + x;
                var nj = j + y;
                
                if (-1 < ni && ni < blockManager.height && -1 < nj && nj < blockManager.width)
                {
                    blockManager.TouchBlock(ni, nj);
                }
            }
        }

        public void ChainBomb(Block block)
        {
            var i = block.id / blockManager.width;
            var j = block.id % blockManager.width;

            var maxCount = -1;
            Vector2? pushPosition = null;

            for (var x = -1; x <= 1; x++)
            for (var y = -1; y <= 1; y++)
            {
                if (Mathf.Abs(x) + Mathf.Abs(y) > 1 || Mathf.Abs(x) + Mathf.Abs(y) == 0) continue;

                var ni = i + x;
                var nj = j + y;

                if (-1 >= ni || ni >= blockManager.height || -1 >= nj || nj >= blockManager.width) continue;
                if (blockManager.Blocks[ni, nj] == null || !blockManager.Blocks[ni, nj].isActiveAndEnabled) continue;

                var count = BlockQueue(new Vector2(ni, nj), false);
                if (count <= maxCount) continue;

                maxCount = count;
                pushPosition = new Vector2(ni, nj);
            }

            if (pushPosition != null)
            {
                BlockQueue((Vector2) pushPosition, true);
            }
        }

        public void VerticalBomb(Block block)
        {
            var i = block.id / blockManager.width;
            var j = block.id % blockManager.width;
            
            for (var x = 0; x < blockManager.height; x++)
            {
                blockManager.TouchBlock(x, j);
            }
        }

        public void HorizontalBomb(Block block)
        {
            var i = block.id / blockManager.width;
            var j = block.id % blockManager.width;
            
            for (var y = 0; y < blockManager.width; y++)
            {
                blockManager.TouchBlock(i, y);
            }
        }

        private int BlockQueue(Vector2 startPosition, bool withDestruction)
        {
            var result = 0; 
            var queue = new Queue<Vector2>();
            var used = new bool[blockManager.height, blockManager.width];
            var blockTypes = blockManager.Types[(int) startPosition.x, (int) startPosition.y];

            queue.Enqueue(startPosition);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                
                for (var x = -1; x <= 1; x++)
                for (var y = -1; y <= 1; y++)
                {
                    if (Mathf.Abs(x) + Mathf.Abs(y) > 1 || Mathf.Abs(x) + Mathf.Abs(y) == 0) continue;

                    var ni = (int) current.x + x;
                    var nj = (int) current.y + y;

                    if (-1 >= ni || ni >= blockManager.height || -1 >= nj || nj >= blockManager.width || used[ni, nj]) continue;
                    if (blockManager.Blocks[ni, nj] == null || blockManager.Types[ni, nj] != blockTypes) continue;
                    
                    result++;
                    used[ni, nj] = true;
                    queue.Enqueue(new Vector2(ni, nj));

                    if (withDestruction)
                    {
                        blockManager.TouchBlock(ni, nj);
                    }
                }
            }
            
            return result;
        }
        
        public void MakeBonusBullet(Block block)
        {
            var bullet = spawnManager.GetBullet();
            
            bullet.transform.SetParent(transform);
            bullet.transform.position = block.transform.position;
            
            bullet.Init(spawnManager, block.type, blockTypeManager.GetData(block.type).bulletAction);
        }
    }
}
