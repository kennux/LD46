using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public ReactorGridUIController reactorGridUI;

    public StoreBoxUIController storeBoxUI;

    public InfoBoxUIController infoBoxUI;

    public SelectedPartLayer selectedPartLayer;

    private Game game;

    private ReactorPart selectedPart;

    private int movingPartFromCellIndex = -1;

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
        if (selectedPart != null)
        {
            var isMovingExistingPart = movingPartFromCellIndex >= 0;
            var canAffordPart = isMovingExistingPart || game.playerMoney >= selectedPart.Def.price;

            if (canAffordPart)
            {
                game.playerMoney -= isMovingExistingPart ? 0 : selectedPart.Def.price;
                
                if (isMovingExistingPart)
                {
                    game.reactor.SetPart(movingPartFromCellIndex, game.reactor.GetPart(cellIndex));
                }
                game.reactor.SetPart(cellIndex, selectedPart);
                
                SetSelectedPart(null);
                movingPartFromCellIndex = -1;
            }
        }
        else
        {
            var partBeingMoved = game.reactor.GetPart(cellIndex);
            movingPartFromCellIndex = partBeingMoved != null ? cellIndex : -1;
            SetSelectedPart(partBeingMoved);
            game.reactor.SetPart(cellIndex, null);
        }
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
        SetSelectedPart(new ReactorPart(part));
    }

    private void SetSelectedPart(ReactorPart part)
    {
        selectedPart = part;
        selectedPartLayer.SetSelectedPart(part);
    }

    private void Update()
    {
        game.Update(Time.deltaTime);

        reactorGridUI.Refresh();
        infoBoxUI.UpdateValues(game.playerMoney, game.currentDemand, game.producedEnergy * Reactor.TicksPerSecond);

        if (CancelSettingPartInput())
        {
            CancelSettingPart();
        }
    }

    private bool CancelSettingPartInput()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    private void CancelSettingPart()
    {
        if (movingPartFromCellIndex >= 0)
        {
            game.reactor.SetPart(movingPartFromCellIndex, selectedPart);
            movingPartFromCellIndex = -1;
        }
        SetSelectedPart(null);
    }
}