﻿using System;
using System.Collections.Generic;
using Logics;
using UnityEngine;

namespace Controllers.Managers
{
    public class BonusManager : MonoBehaviour
    {
        [SerializeField]
        private BlockManager blockManager;

        public static event Action<BlockTypes> OnBulletBonus;

        private void Start()
        {
            OnBulletBonus = null;
            BlockManager.OnBlockDestroy += BonusCheck;
        }

        private void BonusCheck(Block block, BlockTypes blockType)
        {
            var i = block.id / blockManager.width;
            var j = block.id % blockManager.width;
            
            switch (blockType)
            {
                case BlockTypes.Bomb:
                    for (var x = -1; x <= 1; x++)
                    for (var y = -1; y <= 1; y++)
                    {
                        if (Mathf.Abs(x) + Mathf.Abs(y) > 1 || Mathf.Abs(x) + Mathf.Abs(y) == 0) continue;

                        var ni = i + x;
                        var nj = j + y;
                        
                        if (-1 < ni && ni < blockManager.height && -1 < nj && nj < blockManager.width)
                        {
                            blockManager.TouchBlock(ni, nj, 1);
                        }
                    }
                    break;
                
                case BlockTypes.ChainBomb:
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
                    
                    break;
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
                        blockManager.TouchBlock(ni, nj, 2);
                    }
                }
            }
            
            return result;
        }

        public void BulletBonus(BlockTypes blockType)
        {
            OnBulletBonus?.Invoke(blockType);
        }

        private void OnDestroy()
        {
            BlockManager.OnBlockDestroy -= BonusCheck;
        }
    }
}
