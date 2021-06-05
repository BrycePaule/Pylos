using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Map Settings")]
public class MapSettings : ScriptableObject
{
	public int MapSize;

	public float Scale;
	public float OffsetX;
	public float OffsetY;

	public GroundTileData[,] Tiles;

	public GroundTileData GetTile(Vector2Int loc) => Tiles[loc.x, loc.y];

	// Pathing
	public bool IsWithinBounds(Vector2Int loc)
	{
		if (loc.x < 0 || loc.x >= MapSize) { return false; }
		if (loc.y < 0 || loc.y >= MapSize) { return false; }
		return true;
	}
	
	public bool IsPathable(Vector2Int loc,  List<TileTravelType> types)
	{
		if (!IsWithinBounds(loc)) { return false; }

		foreach (TileTravelType type in types)
		{
			if (GetTile(loc).TravelType.Contains(type))
			{
				return true;
			}
		}

		return false;
	}
	
	// Location
	public int RandomIntInBounds()
	{
		return (int) Random.Range(0, MapSize);
	}

	public Vector2Int SelectRandomLocation(List<TileTravelType> travelTypes)
	{
		Vector2Int randomLoc = Vector2Int.zero;

		while (randomLoc == Vector2Int.zero)
		{
			Vector2Int potentialLoc = new Vector2Int(RandomIntInBounds(), RandomIntInBounds());

			if (IsPathable(potentialLoc, travelTypes))
				randomLoc = potentialLoc;
				break;
		}
		return randomLoc;
	}
}
