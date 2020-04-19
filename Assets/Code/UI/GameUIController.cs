using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public ReactorGridUIController ReactorGridUI;

    private Game game;

    private void Start()
    {
        game = new Game();
        
        ReactorGridUI.Initialize(game.reactor);
    }

    private void Update()
    {
        game.Update(Time.deltaTime);
    }
}