using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data Packs/GroundTile")]
public class GroundTile : TileBase
{
	public ColourPalette ColourPalette;

    public Tile Tile;
    public GroundType GroundType;
	public List<TileTravelType> TravelType;
    public List<GameObject> ContainedObjects;

	public Color ColorLookup(GroundType type)
	{
		switch (type)
		{
			case GroundType.Water:
				return ColourPalette.Water;
			case GroundType.Sand:
				return ColourPalette.Sand;
			case GroundType.Dirt:
				return ColourPalette.Dirt;
			case GroundType.Grass:
				return ColourPalette.Grass;
			default:
				return ColourPalette.Stone;
		}
	}
}