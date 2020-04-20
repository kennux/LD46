using UnityEngine;
using UnityEngine.UI;

public class SelectedPartLayer : MonoBehaviour
{
    public Image selectedPartIcon;

    public ReactorPart selectedPart { get; private set; }

    public void SetSelectedPart(ReactorPart part)
    {
        selectedPart = part;
        selectedPartIcon.enabled = selectedPart != null;
        selectedPartIcon.sprite = selectedPart?.Def.uiIcon;
    }

    private void Update()
    {
        if (selectedPartIcon.enabled)
        {
            selectedPartIcon.transform.position = Input.mousePosition;
        }
    }
}