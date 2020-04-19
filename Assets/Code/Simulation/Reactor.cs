using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ReactorTickResult
{
	public float energyProducedMegaWatts;
	public bool reactorExploded;
}

public class Reactor
{
	public const int CellHeatMin = 0;
	public const int CellHeatMax = 1000;
	public const int TicksPerSecond = 60;
	public const float TimePassedPerTick = 1f / TicksPerSecond;

	private int sizeX;
	private int sizeY;

	private ReactorPart[] parts;
	private float[] cellHeats;
	public ReactorPart[] PartsReadOnly => parts;

	public static float GetNormalizedHeat(float heat)
	{
		return heat / (float)CellHeatMax;
	}

	public Reactor(int sizeX, int sizeY)
	{
		this.sizeX = sizeX;
		this.sizeY = sizeY;

		this.parts = new ReactorPart[sizeX * sizeY];
        this.cellHeats = new float[sizeX * sizeY];
    }

	public int GetCellIndex(int x, int y)
	{
		return x * sizeY + y;
	}

	public void GetCellPos(int index, out int x, out int y)
	{
		x = Mathf.FloorToInt(index / (float)sizeY);
		y = index % sizeY;
	}

	public bool IsValidPos(int x, int y)
	{
		return x >= 0 && x < sizeX && y >= 0 && y < sizeY;
	}

	public void SetPart(int x, int y, ReactorPart part)
	{
		SetPart(GetCellIndex(x, y), part);
	}

	public void SetPart(int index, ReactorPart part)
    {
		this.parts[index] = part;
		part?.SetReactor(this);
	}

	public ReactorPart GetPart(int x, int y)
    {
        return GetPart(GetCellIndex(x, y));
    }

    public ReactorPart GetPart(int index)
    {
        return this.parts[index];
    }

	public float GetCellHeat(int x, int y)
	{
		return GetCellHeat(GetCellIndex(x, y));
	}

	public float GetCellHeat(int index)
	{
		return this.cellHeats[index];
	}

	public ReactorTickResult Tick()
	{
		var tickResult = new ReactorTickResult()
		{
			energyProducedMegaWatts = 0,
			reactorExploded = false
		};

		for (int i = 0; i < parts.Length; i++)
		{
			if (parts[i] == null)
				continue;

			var part = parts[i];
			part.Tick(ref tickResult, cellHeats);

			if (part.HasDurability && part.CurrentDurability <= 0)
			{
				parts[i] = null;
			}
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
