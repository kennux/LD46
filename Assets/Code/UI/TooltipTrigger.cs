using UnityEngine;
using UnityEngine.EventSystems;

public abstract class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool _isMouseOver;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isMouseOver = false;
        Tooltip.Close();
    }

    protected virtual void Update()
    {
        if (_isMouseOver)
        {
            Tooltip.Open(GetTooltipText());
        }
    }

    protected abstract string GetTooltipText();

}