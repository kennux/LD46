using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
	private static ReactorPartDef[] partDefs;

	private static bool wasInitialized = false;
	public static void Initialize()
	{
		if (wasInitialized)
			return;

		wasInitialized = true;
		partDefs = Resources.LoadAll<ReactorPartDef>("");
	}

	public static IEnumerable<ReactorPartDef> GetReactorParts()
	{
		Initialize();
		return partDefs;
	}
}
