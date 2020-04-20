using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreBoxEntryUIController : MonoBehaviour
{
    public Image iconImage;

    public TextMeshProUGUI costText;

    public Button clickTrigger;

    public StoreBoxEntryTooltipTrigger tooltipTrigger;

    [TextArea]
    public string costFormat = "{0:0.00} CR";

    public ReactorPartDef part { get; private set; }

    public event Action<ReactorPartDef> selected;

    public void Initialize(ReactorPartDef part)
    {
        this.part = part;
        iconImage.sprite = part.uiIcon;
        costText.text = string.Format(costFormat, part.price);
        clickTrigger.onClick.AddListener(() => selected?.Invoke(this.part));
        tooltipTrigger.part = part;
    }
}