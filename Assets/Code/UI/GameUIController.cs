using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public ReactorGridUIController ReactorGridUI;

    public InfoBoxUIController InfoBoxUI;

    private Game game;

    private void Start()
    {
        game = new Game();
        
        ReactorGridUI.Initialize(game.reactor);
    }

    private void Update()
    {
        game.Update(Time.deltaTime);

        InfoBoxUI.UpdateValues(game.playerMoney, game.currentDemand, game.producedEnergy);
    }
}