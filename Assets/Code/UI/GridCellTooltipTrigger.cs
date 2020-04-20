using UnityEngine;

public class GridCellTooltipTrigger : TooltipTrigger
{
    [TextArea] public string tooltipFormat = "Temperature: {0:0.0} C\nDurability: {1}%";

    public Reactor reactor { get; private set; }

    public int cellIndex { get; private set; }

    public void Initialize(Reactor reactor, int cellIndex)
    {
        this.reactor = reactor;
        this.cellIndex = cellIndex;
    }

    protected override string GetTooltipText()
    {
        var part = reactor.GetPart(cellIndex);
        if (part == null)
        {
            return string.Empty;
        }

        var temperature = reactor.GetCellHeat(cellIndex);
        var durability = Mathf.RoundToInt(part.CurrentDurability / part.Def.durability * 100);
        return string.Format(tooltipFormat, temperature, durability);
    }
}