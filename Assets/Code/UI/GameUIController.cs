using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public ReactorGridUIController reactorGridUI;

    public StoreBoxUIController storeBoxUI;

    public InfoBoxUIController infoBoxUI;

    private Game game;

    private ReactorPartDef selectedPart;

    private void Start()
    {
        game = new Game();
        
        reactorGridUI.Initialize(game.reactor);
        reactorGridUI.cellSelected += OnCellSelected;

        storeBoxUI.Initialize(GameData.GetReactorParts());
        storeBoxUI.partSelected += PartSelected;
    }

    private void OnCellSelected(int cellIndex)
    {
        if (selectedPart == null)
        {
            return;
        }

        if (game.playerMoney < selectedPart.price)
        {
            return;
        }

        if (selectedPart != game.reactor.GetPart(cellIndex)?.Def)
        {
            game.playerMoney -= selectedPart.price;
            game.reactor.SetPart(cellIndex, new ReactorPart(selectedPart));
        }

        selectedPart = null;
    }

    private void PartSelected(ReactorPartDef part)
    {
        selectedPart = part;
    }

    private void Update()
    {
        game.Update(Time.deltaTime);

        reactorGridUI.UpdateParts();
        infoBoxUI.UpdateValues(game.playerMoney, game.currentDemand, game.producedEnergy);
    }
}