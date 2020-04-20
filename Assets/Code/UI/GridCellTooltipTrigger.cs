using UnityEngine;

public class GridCellTooltipTrigger : TooltipTrigger
{
    [TextArea] public string tooltipFormat = "<b>{0}</b>\n\nTemperature: {1:0.0} C\nDurability: {2}%";

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
        return string.Format(tooltipFormat, part.Def.displayName, temperature, durability);
    }
}