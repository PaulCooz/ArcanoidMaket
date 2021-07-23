using System.Collections.Generic;
using ScriptObjects;
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
        [SerializeField] 
        private GameConfig config;
        
        public void Bomb(Block block)
        {
            var i = block.id / blockManager.width;
            var j = block.id % blockManager.width;
            var count = 0;
            
            for (var x = -1; x <= 1; x++)
            for (var y = -1; y <= 1; y++)
            {
                if (Mathf.Abs(x) + Mathf.Abs(y) > 1 || Mathf.Abs(x) + Mathf.Abs(y) == 0) continue;

                var ni = i + x;
                var nj = j + y;
                
                if (-1 < ni && ni < blockManager.height && -1 < nj && nj < blockManager.width)
                {
                    blockManager.RemoveBlock(ni, nj, ++count * config.boomTime);
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
            var up = 1;
            var down = 1;
            var count = 0;
            
            while (-1 < i - down || i + up < blockManager.height)
            {
                if (-1 < i - down)
                {
                    blockManager.RemoveBlock(i - down, j, ++count * config.boomTime);
                    down++;
                }
                if (i + up < blockManager.height)
                {
                    blockManager.RemoveBlock(i + up, j, ++count * config.boomTime);
                    up++;
                }
            }
        }

        public void HorizontalBomb(Block block)
        {
            var i = block.id / blockManager.width;
            var j = block.id % blockManager.width;
            var left = 1;
            var right = 1;
            var count = 0;
            
            while (-1 < j - left || j + right < blockManager.width)
            {
                if (-1 < j - left)
                {
                    blockManager.RemoveBlock(i, j - left, ++count * config.boomTime);
                    left++;
                }

                if (j + right < blockManager.width)
                {
                    blockManager.RemoveBlock(i, j + right, ++count * config.boomTime);
                    right++;
                }
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

                    if (-1 >= ni || ni >= blockManager.height || -1 >= nj || nj >= blockManager.width ||
                        used[ni, nj] || blockManager.Blocks[ni, nj] == null ||
                        !blockManager.Blocks[ni, nj].isActiveAndEnabled || blockManager.Types[ni, nj] != blockTypes)
                    {
                        continue;
                    }
                    
                    result++;
                    used[ni, nj] = true;
                    queue.Enqueue(new Vector2(ni, nj));

                    if (withDestruction)
                    {
                        blockManager.RemoveBlock(ni, nj, result * config.boomTime);
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
