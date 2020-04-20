using UnityEngine;

public class Game
{
	public const int ReactorSizeX = 8;
	public const int ReactorSizeY = 6;
	public const int PlayerStartingMoney = 500;
	public const float DemandIncreasePerMinute = 1f;
	public const float StartingDemand = 1;
	public const float MoneyPerMegawattSecond = 1.1f;

	public float playerMoney = PlayerStartingMoney;
	public float currentDemand = StartingDemand;
    public float producedEnergy;
    public bool reactorExploded;

	public Reactor reactor;
	private float deltaTimeLeft;

	public float timePassed;

	public Game()
	{
		reactor = new Reactor(ReactorSizeX, ReactorSizeY);
	}

	public void Update(float deltaTime)
	{
		deltaTimeLeft += deltaTime;

		while (deltaTimeLeft > Reactor.TimePassedPerTick)
		{
			Tick();
			deltaTimeLeft -= Reactor.TimePassedPerTick;
		}
	}

	public void Tick()
	{
		timePassed += Reactor.TimePassedPerTick;

		currentDemand = StartingDemand + (Mathf.Floor(timePassed / 60f) * DemandIncreasePerMinute);
		var result = reactor.Tick();

		playerMoney += Mathf.Min(result.energyProducedMegaWatts, currentDemand.PerSecondToPerTick()) * MoneyPerMegawattSecond;
        producedEnergy = result.energyProducedMegaWatts;
        reactorExploded = result.reactorExploded;
    }
}