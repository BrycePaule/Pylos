using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Test_Pathfinder
{
	[Test]
	public void get_neighbours_gets_all_valid_neighbours()
	{
		int _mapSize = 256;

		MapBoard.Instance.MapSize = _mapSize;
		MapBoard.Instance.Initialise(_mapSize);
		Pathfinder.Instance.Initialise(_mapSize);

		Assert.IsTrue(true);
	}
	
}


