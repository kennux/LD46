using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TickResult
{
	public int energyProduced;
	public bool reactorExploded;
}

public class Reactor
{
	public const int CellHeatMin = 0;
	public const int CellHeatMax = 1000;
	public const int TicksPerSecond = 60;
	public const float TimePassedPerTick = 1 / TicksPerSecond;

	private int sizeX;
	private int sizeY;

	private ReactorPart[] parts;
	private int[] cellHeats;
	public ReactorPart[] PartsReadOnly => parts;

	public static float GetNormalizedHeat(int heat)
	{
		return heat / (float)CellHeatMax;
	}

	public Reactor(int sizeX, int sizeY)
	{
		this.sizeX = sizeX;
		this.sizeY = sizeY;

		this.parts = new ReactorPart[sizeX * sizeY];
	}

	public int GetCellIndex(int x, int y)
	{
		return x * sizeY + y;
	}

	public void SetPart(int x, int y, ReactorPart part)
	{
		SetPart(GetCellIndex(x, y), part);
	}

	public void SetPart(int index, ReactorPart part)
	{
		this.parts[index] = part;
		part.SetReactor(this);
	}

	public ReactorPart GetPart(int x, int y)
	{
		return this.parts[GetCellIndex(x, y)];
	}

	public int GetCellHeat(int x, int y)
	{
		return GetCellHeat(GetCellIndex(x, y));
	}

	public int GetCellHeat(int index)
	{
		return this.cellHeats[index];
	}

	public TickResult Tick()
	{
		var tickResult = new TickResult()
		{
			energyProduced = 0,
			reactorExploded = false
		};

		foreach (var part in parts)
		{
			part.Tick(ref tickResult, cellHeats);
		}

		// Check for explosion
		for (int i = 0; i < cellHeats.Length; i++)
		{
			if (cellHeats[i] >= CellHeatMax)
				tickResult.reactorExploded = true;
		}

		return tickResult;
	}
}
