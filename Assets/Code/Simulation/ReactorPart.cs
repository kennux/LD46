using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ReactorPart
{
	public const float EnergyProductionDecreaseDueToHeatMax = 0.5f;

	private ReactorPartDef def;
	private int durability;
	private Reactor reactor;

	public ReactorPartDef Def => def;
	public Reactor Reactor => reactor;

	// State
	public int CurrentDurability => durability;
	public float CurrentEnergyProductionPerSecondMegaWatts
	{
		get
		{
			float productionBase = def.energyGenerationMegaWatts - def.energyConsumptionMegaWatts;
			if (Mathf.Approximately(productionBase, 0))
				return 0;

			if (reactor == null)
				return productionBase;

			float invNormalizedHeat = Reactor.GetNormalizedHeat(reactor.GetCellHeat(Array.IndexOf(reactor.PartsReadOnly, this)));
			invNormalizedHeat = 1f - invNormalizedHeat;

			return productionBase * invNormalizedHeat * EnergyProductionDecreaseDueToHeatMax;
		}
	}

	public ReactorPart(ReactorPartDef partDef)
	{
		this.def = partDef;
		this.durability = partDef.durability;
	}

	// This is called from Reactor itself when added.
	public void SetReactor(Reactor reactor)
	{
		this.reactor = reactor;
	}

	public void Tick(ref TickResult tickResult, int[] cellHeats)
	{

	}
}
