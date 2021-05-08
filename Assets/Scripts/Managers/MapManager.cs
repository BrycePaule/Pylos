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
        if (Tiles[loc.x, loc.y].IsWalkable)
        {
            return true;
        } 
        else 
        {
            return false;
        }
    }

	public bool IsSwimmable(Vector2Int loc)
    {
        if (Tiles[loc.x, loc.y].IsSwimmable)
        {
            return true;
        } 
        else 
        {
            return false;
        }
    }

    public GroundTileData GetTile(Vector2Int loc)
    {
        return Tiles[loc.x, loc.y];
    }

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
}
