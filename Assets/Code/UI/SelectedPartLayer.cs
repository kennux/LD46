using UnityEngine;
using UnityEngine.UI;

public class SelectedPartLayer : MonoBehaviour
{
    public Image selectedPartIcon;

    public ReactorPartDef selectedPart { get; private set; }

    public void SetSelectedPart(ReactorPartDef part)
    {
        selectedPart = part;
        selectedPartIcon.enabled = selectedPart != null;
        selectedPartIcon.sprite = selectedPart?.uiIcon;
    }

    private void Update()
    {
        if (selectedPartIcon.enabled)
        {
            selectedPartIcon.transform.position = Input.mousePosition;
        }
    }
}