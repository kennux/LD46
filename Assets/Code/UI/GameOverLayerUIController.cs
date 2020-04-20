using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverLayerUIController : MonoBehaviour
{
    [TextArea] public string timeSurvivedTextFormat = "Time survived: {0:0.0} seconds";

    public GameObject reactorExplodedReasonGameObject;

    public GameObject demandUnmetReasonGameObject;

    public TextMeshProUGUI timeSurvivedText;

    public Button playAgainButton;

    public void Show(GameOverReason reason, int score)
    {
        if (!gameObject.activeSelf)
        {
            reactorExplodedReasonGameObject.SetActive(reason == GameOverReason.ReactorExploded);
            demandUnmetReasonGameObject.SetActive(reason == GameOverReason.DemandUnmetForTooLong);
            timeSurvivedText.text = string.Format(timeSurvivedTextFormat, score);
            gameObject.SetActive(true);
        }
    }

    private void Awake()
    {
        playAgainButton.onClick.AddListener(OnPlayAgain);
    }

    private void OnPlayAgain()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}