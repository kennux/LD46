using UnityEngine;

public static class Tooltip
{
    private static TooltipLayerUIController tooltipLayer;

    public static void Open(string text)
    {
        GetTooltipLayer().Open(text);
    }

    public static void Close()
    {
        GetTooltipLayer().Close();
    }

    private static TooltipLayerUIController GetTooltipLayer()
    {
        if (tooltipLayer == null)
        {
            tooltipLayer = Object.FindObjectOfType<TooltipLayerUIController>();
        }

        return tooltipLayer;
    }
}