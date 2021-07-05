using Libs;

namespace Models
{
    public class GridOfHearts : GridOfObjects<Heart>
    {
        protected override Heart CreateCell(Heart cellObject, int i, int j, string tagObject)
        {
            var position = GetCellPosition(i, j);
            
            cellObject = spawnManager.SpawnHeart(tagObject);
            cellObject.transform.SetParent(transform);
            cellObject.Init(position.x, position.y, _cellSize.x, _cellSize.y, spawnManager, mainCamera);

            return cellObject;
        }
    }
}
