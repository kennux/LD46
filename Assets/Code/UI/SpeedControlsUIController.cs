using UnityEngine;

public class SpeedControlsUIController : MonoBehaviour
{
    public SpeedControlsButton pausedButton;

    public SpeedControlsButton speed1Button;

    public SpeedControlsButton speed2Button;

    public SpeedControlsButton speed3Button;

    public GameSpeed gameSpeed { get; private set; }

    private void Awake()
    {
        pausedButton.click += () => SetGameSpeed(GameSpeed.Paused);
        speed1Button.click += () => SetGameSpeed(GameSpeed.Speed1);
        speed2Button.click += () => SetGameSpeed(GameSpeed.Speed2);
        speed3Button.click += () => SetGameSpeed(GameSpeed.Speed3);
    }

    public void SetGameSpeed(GameSpeed gameSpeed)
    {
        this.gameSpeed = gameSpeed;
        pausedButton.SetActiveState(this.gameSpeed == GameSpeed.Paused);
        speed1Button.SetActiveState(this.gameSpeed == GameSpeed.Speed1);
        speed2Button.SetActiveState(this.gameSpeed == GameSpeed.Speed2);
        speed3Button.SetActiveState(this.gameSpeed == GameSpeed.Speed3);
    }
}