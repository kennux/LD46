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
        this.part = part;
        UpdatePartIcon();
        UpdatePartDurability();
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

    public void SetTemperature(float temperature)
    {
        temperatureMeterImage.color = temperatureGradient.Evaluate(temperature);
    }
}