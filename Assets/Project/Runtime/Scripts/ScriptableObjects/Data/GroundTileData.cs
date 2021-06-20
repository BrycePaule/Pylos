using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class GroundTileData : TileBase
{
	public ColourPalette ColourPalette;

    public Tile Tile;
    public GroundType GroundType;
	public List<TileTravelType> TravelType;
    public List<GameObject> ContainedObjects;

    [MenuItem("Assets/Create/Tiles/GroundTileData")]
    public static void CreateGroundTile(MenuCommand menuCommand)
    {
        string path = EditorUtility.SaveFilePanelInProject("Save GroundTileData", "New GroundTileData", "Asset", "Save GroundTileData", "Assets/Create/Tiles");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<GroundTileData>(), path);
    }

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