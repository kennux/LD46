using UnityEngine;

public class Game
{
	public const int ReactorSizeX = 8;
	public const int ReactorSizeY = 6;
	public const int PlayerStartingMoney = 1000;
	public const float DemandIncreasePerMinute = 0.5f;
	public const float StartingDemand = 1;
	public const float MoneyPerMegawattSecond = 1;

	public float playerMoney = PlayerStartingMoney;
	public float currentDemand = StartingDemand;

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

		playerMoney += result.energyProducedMegaWatts * MoneyPerMegawattSecond.PerSecondToPerTick();
	}
}