using System;
using UnityEngine;

public enum GameOverReason
{
    ReactorExploded,

    DemandUnmetForTooLong
}

public class GameUIController : MonoBehaviour
{
    public float demandNotMetGameOverTime = 20;

    public SpeedControlsUIController speedControls;

    public GameSpeed initialGameSpeed = GameSpeed.Speed1;

    public float gameSpeed1TimeScale = 1;

    public float gameSpeed2TimeScale = 2;

    public float gameSpeed3TimeScale = 4;

    public ReactorGridUIController reactorGridUI;

    public StoreBoxUIController storeBoxUI;

    public InfoBoxUIController infoBoxUI;

    public SelectedPartLayer selectedPartLayer;

    public GameOverLayerUIController gameOverLayer;

    private Game game;

    private ReactorPart selectedPart;

    private int movingPartFromCellIndex = -1;

    private int requestedRemovePartCellIndex = -1;

    private float timeSurvived;

    private float timeDemandNotMet;

    private void Start()
    {
        game = new Game();

        speedControls.SetGameSpeed(initialGameSpeed);
        
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
        requestedRemovePartCellIndex = cellIndex;
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
        if (IsGameOver(out var reason))
        {
            SetSelectedPart(null);
            gameOverLayer.Show(reason, (int) (timeSurvived * 10));
            return;
        }

        UpdateSimulation();

        reactorGridUI.Refresh();
        infoBoxUI.UpdateValues(
            game.playerMoney, 
            Round(game.currentDemand),
            Round(game.producedEnergy * Reactor.TicksPerSecond));

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonUp(1) && selectedPart != null)
        {
            CancelSettingPart();
        }
        else if (requestedRemovePartCellIndex >= 0)
        {
            game.reactor.SetPart(requestedRemovePartCellIndex, null);
        }

        requestedRemovePartCellIndex = -1;
    }

    private float Round(float value)
    {
        return 0.001f * Mathf.RoundToInt(1000 * value);
    }

    private void UpdateSimulation()
    {
        var dt = Time.deltaTime * GetGameSpeedTimeScale();

        game.Update(dt);

        timeSurvived += dt;

        if (Round(game.producedEnergy * Reactor.TicksPerSecond) < Round(game.currentDemand))
        {
            timeDemandNotMet += dt;
        }
        else
        {
            timeDemandNotMet = 0;
        }
    }

    private float GetGameSpeedTimeScale()
    {
        switch (speedControls.gameSpeed)
        {
            case GameSpeed.Paused:
                return 0;

            case GameSpeed.Speed1:
                return gameSpeed1TimeScale;

            case GameSpeed.Speed2:
                return gameSpeed2TimeScale;

            case GameSpeed.Speed3:
                return gameSpeed3TimeScale;
        }

        throw new ArgumentOutOfRangeException();
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

    private bool IsGameOver(out GameOverReason reason)
    {
        if (game.reactorExploded)
        {
            reason = GameOverReason.ReactorExploded;
            return true;
        }

        if (timeDemandNotMet >= demandNotMetGameOverTime)
        {
            reason = GameOverReason.DemandUnmetForTooLong;
            return true;
        }

        reason = default;
        return false;
    }
}