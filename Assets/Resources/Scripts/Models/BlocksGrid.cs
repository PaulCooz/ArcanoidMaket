using UnityEngine;
using UnityEngine.UI;

public class BlocksGrid : MonoBehaviour
{
    public GridLayoutGroup grid;
    public RectTransform gridTransform;

    void Start()
    {
        SetNewGrid(4, 3);
    }

    private void SetNewGrid(int rows, int columns)
    {
        var rect = gridTransform.rect;
        var gridSize = new Vector2(rect.width, rect.height);
        
        gridSize.x -= 10 * (columns + 1);
        gridSize.y -= 10 * (rows + 1);
        
        grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        grid.constraintCount = rows;

        gridSize.x /= columns;
        gridSize.y /= rows;
        grid.cellSize = gridSize;
    }
}
