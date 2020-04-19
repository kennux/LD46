using UnityEngine;
using UnityEngine.UI;

public class ReactorGridUIController : MonoBehaviour
{
    public GridLayoutGroup gridLayout;

    public ReactorGridCellUIController cellPrefab;

    private Reactor reactor;

    public void Initialize(Reactor reactor)
    {
        this.reactor = reactor;

        SetupGridLayout();
        CreateCells();
    }

    private void SetupGridLayout()
    {
        gridLayout.constraintCount = Game.ReactorSizeX;
        
        var gridRect = ((RectTransform) gridLayout.transform).rect;
        var cellSize = Mathf.Min(gridRect.width / Game.ReactorSizeX, gridRect.height / Game.ReactorSizeY);
        gridLayout.cellSize = new Vector2(cellSize, cellSize);
    }

    private void CreateCells()
    {
        for (var y = 0; y < Game.ReactorSizeY; y++)
        {
            for (var x = 0; x < Game.ReactorSizeX; x++)
            {
                Instantiate(cellPrefab, gridLayout.transform);
            }
        }
    }
}