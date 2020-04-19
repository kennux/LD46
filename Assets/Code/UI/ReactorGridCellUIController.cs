using System;
using UnityEngine;
using UnityEngine.UI;

public class ReactorGridCellUIController : MonoBehaviour
{
    public Image partIcon;

    public ClickTrigger clickTrigger;

    public int cellIndex { get; private set; }

    public ReactorPart part { get; private set; }

    public event Action<int> leftClick;

    public event Action<int> rightClick;

    public void Initialize(int cellIndex)
    {
        this.cellIndex = cellIndex;
        clickTrigger.click += OnClick;
        UpdatePartIcon();
    }

    private void OnClick(int button)
    {
        switch (button)
        {
            case 0:
                leftClick?.Invoke(cellIndex);
                break;

            case 1:
                rightClick?.Invoke(cellIndex);
                break;
        }
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