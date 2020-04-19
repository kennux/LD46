using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorPartDef : ScriptableObject
{
	// General
	public string displayName;
	public string tooltip;
	public int durability = -1; // -1 = infinite
	public Sprite uiIcon;

	// Energy (everything is in MW/S)
	public float energyGenerationMegaWatts = 0;
	public float energyConsumptionMegaWatts = 0;

	// Heat management (everything is in heat / s)
	public float heatExchangeRate = 0;
	public float heatRemoval = 0;
	public float durabilityLossPerHeat = 0;

	// Misc
	public float nearbyEnergyProductionBoost = 0;
	public float durabilityLossPerSecond = 0;
}
