using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Map Settings")]
public class MapSettings : ScriptableObject
{
	public float Seed;
	public int MapSize;

	[Header("Noise Generation")]
	public float Scale;
	public float OffsetX;
	public float OffsetY;

	[Header("Terrain Generation")]
	[Range(0, 1f)] public float WaterMaxHeight;
	[Range(0, 1f)] public float SandMaxHeight;
	[Range(0, 1f)] public float DirtMaxHeight;
	[Range(0, 1f)] public float GrassMaxHeight;

	[Header("Object Spawning")]
	[Range(0, 100)] public int TreeSpawnPercent; 
	[Range(0, 100)] public int ShrubSpawnPercent; 
	[Range(0, 100)] public int SandSpotSpawnPercent; 
	[Range(0, 100)] public int DirtSpotSpawnPercent; 
	[Range(0, 100)] public int StoneSpawnPercent; 

	public GroundTileData[,] Tiles;

	public GroundTileData GetTile(Vector2Int loc) => Tiles[loc.x, loc.y];

	// Pathing
	public bool IsWithinBounds(Vector2Int loc)
	{
		if (loc.x < 0 || loc.x >= MapSize) { return false; }
		if (loc.y < 0 || loc.y >= MapSize) { return false; }
		return true;
	}
	
	public bool IsPathable(Vector2Int loc,  List<TileTravelType> travelTypes)
	{
		if (!IsWithinBounds(loc)) { return false; }

		foreach (TileTravelType travelType in travelTypes)
		{
			if (GetTile(loc).TravelType.Contains(travelType))
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
