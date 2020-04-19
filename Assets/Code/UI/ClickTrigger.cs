using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickTrigger : MonoBehaviour, IPointerClickHandler
{
    public event Action<int> click;

    public void OnPointerClick(PointerEventData eventData)
    {
        click?.Invoke((int) eventData.button);
    }
}