using System;
using UnityEngine;
using UnityEngine.UI;

public class ReactorGridUIController : MonoBehaviour
{
    public GridLayoutGroup gridLayout;

    public ReactorGridCellUIController cellPrefab;

    public event Action<int> cellSelected;

    private Reactor reactor;

    private ReactorGridCellUIController[] cells;

    public void Initialize(Reactor reactor)
    {
        this.reactor = reactor;
        InitializeGridLayout();
        InitializeCells();
    }

    private void InitializeGridLayout()
    {
        gridLayout.constraintCount = Game.ReactorSizeX;
        var gridRect = ((RectTransform) gridLayout.transform).rect;
        var cellSize = Mathf.Min(gridRect.width / Game.ReactorSizeX, gridRect.height / Game.ReactorSizeY);
        gridLayout.cellSize = new Vector2(cellSize, cellSize);
    }

    private void InitializeCells()
    {
        cells = new ReactorGridCellUIController[Game.ReactorSizeX * Game.ReactorSizeY];
        for (var cellIndex = 0; cellIndex < cells.Length; cellIndex++)
        {
            var cell = Instantiate(cellPrefab, gridLayout.transform);
            cell.Initialize(cellIndex);
            cell.selected += OnCellSelected;

            cells[cellIndex] = cell;
        }
    }

    private void OnCellSelected(int cellIndex)
    {
        cellSelected?.Invoke(cellIndex);
    }

    public void UpdateParts()
    {
        foreach (var cell in cells)
        {
            cell.UpdatePart(reactor.GetPart(cell.cellIndex));
        }
    }
}