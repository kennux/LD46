using System;
using UnityEngine;
using UnityEngine.UI;

public class ReactorGridCellUIController : MonoBehaviour
{
    public Image partIcon;

    public Button clickTrigger;

    public event Action<int> selected;

    public int cellIndex { get; private set; }

    public ReactorPart part { get; private set; }

    public void Initialize(int cellIndex)
    {
        this.cellIndex = cellIndex;
        clickTrigger.onClick.AddListener(() => selected?.Invoke(this.cellIndex));
        UpdatePartIcon();
    }

    public void UpdatePart(ReactorPart part)
    {
        if (this.part == part)
        {
            return;
        }

        this.part = part;
        UpdatePartIcon();
    }

    private void UpdatePartIcon()
    {
        partIcon.enabled = this.part != null;
        partIcon.sprite = this.part?.Def.uiIcon;
    }
}