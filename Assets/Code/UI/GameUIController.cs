using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public ReactorGridUIController ReactorGridUI;

    public StoreBoxUIController StoreBoxUI;

    public InfoBoxUIController InfoBoxUI;

    private Game game;

    private void Start()
    {
        game = new Game();
        
        ReactorGridUI.Initialize(game.reactor);
        StoreBoxUI.Initialize(GameData.GetReactorParts());
        StoreBoxUI.partSelected += PartSelected;
    }

    private void PartSelected(ReactorPartDef part)
    {
        Debug.Log($"Part selected: {part.displayName}");
    }

    private void Update()
    {
        game.Update(Time.deltaTime);

        InfoBoxUI.UpdateValues(game.playerMoney, game.currentDemand, game.producedEnergy);
    }
}