using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class TooltipLayerUIController : MonoBehaviour
{
    public TooltipUIController tooltip;

    public RectTransform tooltipAnchor;

    private RectTransform tooltipRect;

    private Vector2 tooltipOffset;

    private void Awake()
    {
        tooltipRect = (RectTransform) tooltip.transform;
        tooltipOffset = tooltipRect.anchoredPosition;
    }

    public void Open(string text)
    {
        tooltip.gameObject.SetActive(!string.IsNullOrEmpty(text));
        tooltip.text = text;
    }

    public void Close()
    {
        tooltip.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (tooltip.gameObject.activeSelf)
        {
            var fixPositionOffset = Mathf.Max(Input.mousePosition.x + tooltipOffset.x + tooltipRect.rect.width - Screen.width, 0);
            tooltipRect.anchoredPosition = new Vector2(tooltipOffset.x - fixPositionOffset, tooltipRect.anchoredPosition.y);

            tooltipAnchor.position = Input.mousePosition;
        }
    }
}