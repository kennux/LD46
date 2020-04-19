using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReactorPart", menuName = "Reactor part")]
public class ReactorPartDef : ScriptableObject
{
	[Header("General")]
	public string displayName;
	public string tooltip;
	public float durability = -1; // -1 = infinite
	public Sprite uiIcon;
	public float price;

	[Header("Energy (MW/s)")]
	public float energyGenerationMegaWatts = 0;
	public float energyConsumptionMegaWatts = 0;

	[Header("Heat (Heat/s)")]
	public float heatNeighborPullRate = 0;
	public float heatGeneration = 0;
	public float heatNeighborRemovalRate = 0;
	public float durabilityLossPerHeat = 0;

	[Header("Misc")]
	public float nearbyEnergyProductionBoost = 0;
	public float durabilityLossPerSecond = 0;

	public void OnValidate()
	{
		if (heatNeighborPullRate < 0)
			Debug.LogWarning("Heat exchange rate cannot be less than 0!");
	}
}
