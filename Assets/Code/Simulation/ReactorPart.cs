using System;
using UnityEngine;

public class ReactorPart
{
	public const float EnergyProductionDecreaseDueToHeatMax = 0.5f;

	private ReactorPartDef def;
	private float durability;
	private Reactor reactor;

	public ReactorPartDef Def => def;
	public Reactor Reactor => reactor;

	// State
	public float CurrentDurability => durability;
	public bool HasDurability => def.durability > -1;
	public float CurrentEnergyProductionPerSecondMegaWatts
	{
		get
		{
			float productionBase = def.energyGenerationMegaWatts - def.energyConsumptionMegaWatts;
			if (Mathf.Approximately(productionBase, 0))
				return 0;

			if (reactor == null)
				return productionBase;

			// Nearby production boosts
			int x, y, myIndex = Array.IndexOf(reactor.PartsReadOnly, this);
			reactor.GetCellPos(myIndex, out x, out y);
			float boost = 1;
			for (int i = 0; i < Utility.NeighborOffsetX.Length; i++)
			{
				int oX = Utility.NeighborOffsetX[i], oY = Utility.NeighborOffsetY[i];
				int nX = x + oX;
				int nY = y + oY;
				if (!reactor.IsValidPos(nX, nY))
					continue;

				var part = reactor.GetPart(nX, nY);
				if (part == null)
					continue;

				boost += part.NearbyEnergyProductionBoost;
			}

			float normalizedHeat = Reactor.GetNormalizedHeat(reactor.GetCellHeat(Array.IndexOf(reactor.PartsReadOnly, this)));

			float productionDecreasePercentage = Mathf.Lerp(1, EnergyProductionDecreaseDueToHeatMax, normalizedHeat);
			return productionBase * productionDecreasePercentage * boost;
		}
	}
	public bool ProducesHeat => def.heatGeneration > 0;
	public bool PullsHeat => def.heatNeighborPullRate > 0;
	public bool HeatCanBePulledFrom => ProducesHeat || PullsHeat;
	public float NearbyEnergyProductionBoost => def.nearbyEnergyProductionBoost;

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

	public void Tick(ref ReactorTickResult tickResult, float[] cellHeats)
	{
		int x, y, myIndex = Array.IndexOf(reactor.PartsReadOnly, this);
		reactor.GetCellPos(myIndex, out x, out y);

		durability -= def.durabilityLossPerSecond.PerSecondToPerTick();
		tickResult.energyProducedMegaWatts += CurrentEnergyProductionPerSecondMegaWatts.PerSecondToPerTick();

		// Neighbor heat pulling and removal
		if (!Mathf.Approximately(def.heatNeighborPullRate, 0) || !Mathf.Approximately(def.heatNeighborRemovalRate, 0))
		{
			float pullRate = def.heatNeighborPullRate.PerSecondToPerTick();
			float removeRate = def.heatNeighborRemovalRate.PerSecondToPerTick();

			float pulledHeat = 0, removedHeat = 0;
			// Pull neighbors heat
			for (int i = 0; i < Utility.NeighborOffsetX.Length; i++)
			{
				int oX = Utility.NeighborOffsetX[i], oY = Utility.NeighborOffsetY[i];
				int nX = x + oX;
				int nY = y + oY;
				if (!reactor.IsValidPos(nX, nY))
					continue;

				int idx = reactor.GetCellIndex(nX, nY);
				var part = reactor.GetPart(nX, nY);

				if ((part == null || part.HeatCanBePulledFrom) && cellHeats[idx] > 0)
				{
					float pull = Mathf.Clamp(cellHeats[idx], 0, pullRate);
					pulledHeat += pull;
					cellHeats[idx] -= pull;

					float remove = Mathf.Clamp(cellHeats[idx], 0, removeRate);
					removedHeat += remove;
					cellHeats[idx] -= remove;
				}
			}

			cellHeats[myIndex] += pulledHeat;
			durability -= (pulledHeat + removedHeat) * def.durabilityLossPerHeat;
		}

		// Heat generation / removal
		if (!Mathf.Approximately(def.heatGeneration, 0))
			cellHeats[myIndex] += def.heatGeneration.PerSecondToPerTick();
	}
}
