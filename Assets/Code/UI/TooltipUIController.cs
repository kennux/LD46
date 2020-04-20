using TMPro;
using UnityEngine;

public class TooltipUIController : MonoBehaviour
{
    public TextMeshProUGUI tooltipText;

    public string text
    {
        get => tooltipText.text;
        set => tooltipText.text = value;
    }
}