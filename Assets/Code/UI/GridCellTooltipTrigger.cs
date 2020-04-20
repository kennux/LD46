using UnityEngine;

public class GridCellTooltipTrigger : TooltipTrigger
{
    [TextArea] public string tooltipFormat = "<b>{0}</b>\n\nTemperature: {1:0.0} C";
    [TextArea] public string tooltipFormatDurability = "\nDurability: {0}%";

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
        string text =  string.Format(tooltipFormat, part.Def.displayName, temperature);

        if (part.HasDurability)
        {
            var durability = Mathf.RoundToInt(part.CurrentDurability / part.Def.durability * 100);
            text += string.Format(tooltipFormatDurability, durability);
        }

        return text;
    }
}