using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private Tilemap tilemap;

    public GroundTileData[,] Tiles;
	public Vector2Int PlayerSpawn;

    private void Awake()
    {
        Tiles = new GroundTileData[mapGenerator.MapSize, mapGenerator.MapSize];
    }

	private void Start() 
	{
		SelectPlayerSpawn();	
	}

    public bool IsWalkable(Vector2Int loc)
	{
		if (!IsWithinBounds(loc)) { return false; }

		return Tiles[loc.x, loc.y].IsWalkable ? true : false;
	}
	
	public bool IsSwimmable(Vector2Int loc)
	{
		if (!IsWithinBounds(loc)) { return false; }

		return Tiles[loc.x, loc.y].IsSwimmable ? true : false;
	}

	public bool IsPathable(Vector2Int loc,  List<TileTravelType> types)
	{
		foreach (TileTravelType type in types)
		{
			if (type == TileTravelType.Walkable)
			{
				if (IsWalkable(loc)) { return true; }
			}

			if (type == TileTravelType.Swimmable)
			{
				if (IsSwimmable(loc)) { return true; }
			}
		}

		return false;
	}

	public GroundTileData GetTile(Vector2Int loc) => Tiles[loc.x, loc.y];

	private void SelectPlayerSpawn()
	{
		while (PlayerSpawn == Vector2Int.zero)
		{
			Vector2Int spawnCell = new Vector2Int(mapGenerator.RandomIntInBounds(), mapGenerator.RandomIntInBounds());
			if (Tiles[spawnCell.x, spawnCell.y].IsWalkable)
			{
				PlayerSpawn = spawnCell;
				return;
			}
		}
	}

	public bool IsWithinBounds(Vector2Int loc)
	{
		if (loc.x < 0 || loc.x >= mapGenerator.MapSize) { return false; }
		if (loc.y < 0 || loc.y >= mapGenerator.MapSize) { return false; }
		return true;
	}

	public bool IsWithinBounds(int val)
	{
		if (val < 0 || val >= mapGenerator.MapSize) { return false; }
		return true;
	}
}
