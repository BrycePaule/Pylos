using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class GroundTileData : TileBase
{
    public Tile Tile;
    public List<GameObject> ContainedObjects;
    public GroundType GroundType;
	public List<TileTravelType> TravelType;

	public Color WaterColor;
	public Color SandColor;
	public Color DirtColor;
	public Color GrassColor;
	public Color StoneColor;

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
				return WaterColor;
			case GroundType.Sand:
				return SandColor;
			case GroundType.Dirt:
				return DirtColor;
			case GroundType.Grass:
				return GrassColor;
			default:
				return StoneColor;
		}
	}
}