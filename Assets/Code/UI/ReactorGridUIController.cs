using System;
using UnityEngine;
using UnityEngine.UI;

public class ReactorGridUIController : MonoBehaviour
{
    public GridLayoutGroup gridLayout;

    public ReactorGridCellUIController cellPrefab;

    private Reactor reactor;

    private ReactorGridCellUIController[] cells;

    public event Action<int> cellLeftClick;

    public event Action<int> cellRightClick;
    
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
            cell.leftClick += OnCellLeftClick;
            cell.rightClick += OnCellRightClick;

            cells[cellIndex] = cell;
        }
    }

    private void OnCellLeftClick(int cellIndex)
    {
        cellLeftClick?.Invoke(cellIndex);
    }

    private void OnCellRightClick(int celIndex)
    {
        cellRightClick?.Invoke(celIndex);
    }

    public void UpdateParts()
    {
        foreach (var cell in cells)
        {
            cell.UpdatePart(reactor.GetPart(cell.cellIndex));
        }
    }
}