public class StoreBoxEntryTooltipTrigger : TooltipTrigger
{
    public string tooltipFormat = "<b>{0}</b>\n\n{1}";

    public ReactorPartDef part { get; set; }

    protected override string GetTooltipText()
    {
        return string.Format(tooltipFormat, part.displayName, part.tooltip);
    }
}