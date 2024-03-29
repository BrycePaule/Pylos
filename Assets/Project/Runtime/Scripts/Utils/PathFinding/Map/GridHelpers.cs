using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHelpers : MonoBehaviour
{
	public static bool IsWithinDistance(Vector2Int loc1, Vector2Int loc2, int distance)
	{
		if (Mathf.Abs(loc1.x - loc2.x) > distance) { return false; }
		if (Mathf.Abs(loc1.y - loc2.y) > distance) { return false; }
		return true;
	}

	public static bool IsAtLocation(Vector2Int loc1, Vector2Int loc2)
	{
		return loc1 == loc2;
	}

	public static Vector2Int SpiralSearch(int itemID, Vector2Int currentLoc, int searchDistance)
	{
		// searches a searchDistance x searchDistance square around the location for something of type obj
		// does this by expanding rings around the currentLoc - starting with the smallest 3x3 ring around the location, out to searchDistance range

		int offset = 1;
		while (offset <= searchDistance)
		{
			for (int y = currentLoc.y - offset; y <= currentLoc.y + offset; y++)
			{
				for (int x = currentLoc.x - offset; x <= currentLoc.x + offset; x++)
				{
					if (y == (currentLoc.y - offset) || y == (currentLoc.y + offset) || x == (currentLoc.x - offset) || x == (currentLoc.x + offset))
					{
						Vector2Int searchLoc = new Vector2Int(x, y);
						if (!IsWithinBounds(searchLoc)) { continue; }
						if (searchLoc == currentLoc) { continue; }

						foreach (GameObject obj in MapBoard.Instance.Tiles[x, y].ContainedObjects)
						{
							Container container;
							if (obj.TryGetComponent<Container>(out container))
							{
								if (container.Contains(itemID))
								{
									return searchLoc;
								}
							}
						}
					}
				}
			}
			offset++;
		}
		return new Vector2Int(-1, -1);
	}

	public static bool IsWithinBounds(Vector2Int loc)
	{
		if (loc.x < 0 || loc.x >= 256) { return false; }
		if (loc.y < 0 || loc.y >= 256) { return false; }
		return true;
	}

	public static float ScaleValue(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
	{
		float OldRange = (OldMax - OldMin);
		float NewRange = (NewMax - NewMin);
		return (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
	}

}