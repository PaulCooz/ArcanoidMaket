using System.Collections.Generic;
using Resources.Scripts.Libs;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.Models
{
    public class BlocksGrid : MonoBehaviour
    {
        public GridLayoutGroup grid;
        public RectTransform gridTransform;
        public SpawnerManager spawnerManager;

        public void SetNewGrid(LevelData levelData)
        {
            SetNewSize(levelData.height, levelData.width);
            FillGrid(levelData.data);
        }

        private void SetNewSize(int rows, int columns)
        {
            var rect = gridTransform.rect;
            var gridSize = new Vector2(rect.width, rect.height);
        
            gridSize.x -= grid.spacing.x * (columns + 1);
            gridSize.y -= grid.spacing.y * (rows + 1);
            gridSize.x /= columns;
            gridSize.y /= rows;
        
            grid.padding.left = (int)grid.spacing.x;
            grid.padding.top = (int)grid.spacing.y;
            grid.cellSize = gridSize;
        }

        private void FillGrid(IEnumerable<int> blocks)
        {
            foreach (var blockId in blocks)
            {
                var newBlock = spawnerManager.SpawnFromPool(blockId, transform);
                newBlock.transform.SetParent(transform);
                newBlock.transform.localScale = Vector3.one;
            }
        }
    }
}
