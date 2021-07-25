using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blackboards/MapBoard")]
public class MapBoard : Blackboard
{
	private static MapBoard _instance;

	public int MapSize;
	public GroundTile[,] Tiles;

	public static MapBoard Instance
	{
		get
		{
			if (!_instance)
				_instance = FindObjectOfType<MapBoard>();
			if (!_instance)
				_instance = CreateInstance<MapBoard>();
			return _instance;
		}
	}

	public void Initialise(int mapSize)
	{
		Tiles = new GroundTile[mapSize, mapSize];
	}

	// PATHFINDING

	public GroundTile GetTile(Vector2Int loc)
	{
		return Tiles[loc.x, loc.y];
	}

	public bool IsWithinBounds(Vector2Int loc)
	{
		if (loc.x < 0 || loc.x >= MapSize) { return false; }
		if (loc.y < 0 || loc.y >= MapSize) { return false; }
		return true;
	}
	
	public bool IsPathable(Vector2Int loc,  List<TileTravelType> travelTypes)
	{
		if (!IsWithinBounds(loc)) { return false; }
		if (GetTile(loc).TravelTypes.Contains(TileTravelType.Impassable)) { return false; }

		foreach (TileTravelType travelType in travelTypes)
		{
			if (GetTile(loc).TravelTypes.Contains(travelType))
			{
				return true;
			}
		}

		return false;
	}
	
	// Location
	public int RandomIntInBounds()
	{
		return Random.Range(0, MapSize);
	}

	public Vector2Int SelectRandomLocation(List<TileTravelType> travelTypes)
	{
		Vector2Int randomLoc = Vector2Int.zero;

		while (randomLoc == Vector2Int.zero)
		{
			Vector2Int potentialLoc = new Vector2Int(RandomIntInBounds(), RandomIntInBounds());

			if (IsPathable(potentialLoc, travelTypes))
			{
				randomLoc = potentialLoc;
				break;
			}
		}

		return randomLoc;
	}
}
