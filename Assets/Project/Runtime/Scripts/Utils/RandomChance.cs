using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomChance
{

	public static bool Roll(int chance)
	{
		return Random.Range(0, 100) < chance;
	}
	
	public static bool Roll(float chance)
	{
		return Random.Range(0, 100) < chance;
	}

}
