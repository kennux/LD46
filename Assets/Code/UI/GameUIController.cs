using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public ReactorGridUIController reactorGridUI;

    public StoreBoxUIController storeBoxUI;

    public InfoBoxUIController infoBoxUI;

    public SelectedPartLayer selectedPartLayer;

    private Game game;

    private ReactorPartDef selectedPart;

    private void Start()
    {
        game = new Game();
        
        reactorGridUI.Initialize(game.reactor);
        reactorGridUI.cellLeftClick += OnCellLeftClick;
        reactorGridUI.cellRightClick += OnCellRightClick;

        storeBoxUI.Initialize(GameData.GetReactorParts());
        storeBoxUI.partSelected += OnPartSelected;
    }

    private void OnCellLeftClick(int cellIndex)
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

        SetSelectedPart(null);
    }

    private void OnCellRightClick(int cellIndex)
    {
        if (selectedPart != null)
        {
            return;
        }

        game.reactor.SetPart(cellIndex, null);
    }

    private void OnPartSelected(ReactorPartDef part)
    {
        SetSelectedPart(part);
    }

    private void SetSelectedPart(ReactorPartDef part)
    {
        selectedPart = part;
        selectedPartLayer.SetSelectedPart(part);
    }

    private void Update()
    {
        game.Update(Time.deltaTime);

        reactorGridUI.UpdateParts();
        infoBoxUI.UpdateValues(game.playerMoney, game.currentDemand, game.producedEnergy * Reactor.TicksPerSecond);

        if (CancelBuyPartInput())
        {
            SetSelectedPart(null);
        }
    }

    private bool CancelBuyPartInput()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
}