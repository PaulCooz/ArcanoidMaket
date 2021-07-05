using Libs;

namespace Models
{
    public class GridOfBlocks : GridOfObjects<Block>
    {
        protected override Block CreateCell(Block cellObject, int i, int j, string tagObject)
        {
            var position = GetCellPosition(i, j);
            
            cellObject = spawnManager.SpawnBlock(tagObject);
            cellObject.transform.SetParent(transform);
            cellObject.Init(position.x, position.y, _cellSize.x, _cellSize.y, spawnManager, mainCamera);

            return cellObject;
        }
    }
}
