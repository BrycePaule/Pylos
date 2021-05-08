using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileConversion
{

	// WORLD -> TILE
	public static Vector2Int WorldToTile(Vector3 point)
    {
		return new Vector2Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y));
    }

    public static Vector2Int WorldToTile(Vector2 point)
    {
		return new Vector2Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y));
    }


	// TILE -> WORLD
	public static Vector2 TileToWorld2D(Vector2Int point) 
	{
        return new Vector2(point.x + 0.5f, point.y + 0.5f);
	}

	public static Vector3 TileToWorld3D(Vector2Int point) 
	{
		return new Vector3(point.x + 0.5f, point.y + 0.5f, 0f);
	}

}
