using UnityEngine;

public static class Utility
{
	public static readonly int[] NeighborOffsetX = new int[]
	{
		-1,
		1,
		0,
		0
	};
	public static readonly int[] NeighborOffsetY = new int[]
	{
		0,
		0,
		-1,
		1
	};

	public static float PerSecondToPerTick(this int value)
	{
		return value * Reactor.TimePassedPerTick;
	}
	public static float PerSecondToPerTick(this float value)
	{
		return value * Reactor.TimePassedPerTick;
	}
}