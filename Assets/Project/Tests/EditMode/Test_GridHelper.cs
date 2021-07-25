using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Test_GridHelper
{
	[Test]
	public void is_at_location_passes_for_same_positive_vectors()
	{
		Vector2Int vector1 = new Vector2Int(1, 2);
		Vector2Int vector2 = new Vector2Int(1, 2);

		Assert.IsTrue(GridHelpers.IsAtLocation(vector1, vector2));
	}

	[Test]
	public void is_at_location_passes_for_same_negative_vectors()
	{
		Vector2Int vector1 = new Vector2Int(-1, -5);
		Vector2Int vector2 = new Vector2Int(-1, -5);

		Assert.IsTrue(GridHelpers.IsAtLocation(vector1, vector2));
	}


	[Test]
	public void is_at_location_fails_for_different_vectors()
	{
		Vector2Int vector1 = new Vector2Int(1, 5);
		Vector2Int vector2 = new Vector2Int(2, -5);

		Assert.IsFalse(GridHelpers.IsAtLocation(vector1, vector2));
	}

}
