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
            var rect = gridTransform.rect;
            var cellSize = new Vector2(rect.width, rect.height);

            var columns = levelData.width;
            var rows = levelData.height;
        
            cellSize.x -= grid.spacing.x * (columns + 1);
            cellSize.y -= grid.spacing.y * (rows + 1);
            cellSize.x /= columns;
            cellSize.y /= rows;
        
            grid.padding.left = (int)grid.spacing.x;
            grid.padding.top = (int)grid.spacing.y;
            grid.cellSize = cellSize;
            
            foreach (var blockId in levelData.data)
            {
                var newBlock = spawnerManager.SpawnFromPool(blockId);
                
                newBlock.transform.SetParent(transform);
                newBlock.transform.localScale = Vector3.one;

                newBlock.Init(cellSize, 5);
            }
        }
    }
}
