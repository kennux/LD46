using System;
using UnityEngine;
using UnityEngine.UI;

public class ReactorGridCellUIController : MonoBehaviour
{
    public Image partIcon;

    public GameObject durabilityGameObject;

    public Slider durabilitySlider;

    public Image temperatureMeterImage;

    public Gradient temperatureGradient;

    public ClickTrigger clickTrigger;

    public GridCellTooltipTrigger tooltipTrigger;

    public Reactor reactor { get; private set; }

    public int cellIndex { get; private set; }

    public ReactorPart part => reactor.GetPart(cellIndex);

    public event Action<int> leftClick;

    public event Action<int> rightClick;

    public void Initialize(Reactor reactor, int cellIndex)
    {
        this.reactor = reactor;
        this.cellIndex = cellIndex;
        clickTrigger.click += OnClick;
        tooltipTrigger.Initialize(this.reactor, this.cellIndex);
        Refresh();
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

    public void Refresh()
    {
        UpdatePartIcon();
        UpdatePartDurability();
        UpdatePartTemperature();
    }

    private void UpdatePartIcon()
    {
        partIcon.enabled = this.part != null;
        partIcon.sprite = this.part?.Def.uiIcon;
    }

    private void UpdatePartDurability()
    {
        durabilityGameObject.SetActive(part != null && part.HasDurability);
        durabilitySlider.value = part != null ? part.CurrentDurability / part.Def.durability : 0;
    }

    public void UpdatePartTemperature()
    {
        var temperature = Reactor.GetNormalizedHeat(reactor.GetCellHeat(cellIndex));
        temperatureMeterImage.color = temperatureGradient.Evaluate(temperature);
    }
}