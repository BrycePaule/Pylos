using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class GroundTileData : TileBase
{
    public Tile Tile;
    public bool IsWalkable;
    public bool IsSwimmable;
    public List<GameObject> ContainedObjects;
    public GroundType GroundType;
	public TileTravelType TravelType;

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
    }

    public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
    {
        return base.GetTileAnimationData(position, tilemap, ref tileAnimationData);
    }

    // public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    // {
    //     return base.StartUp(position, tilemap, go);
    // }

    // MENU STUFF 
    [MenuItem("Assets/Create/Tiles/GroundTileData")]
    public static void CreateGroundTile(MenuCommand menuCommand)
    {
        string path = EditorUtility.SaveFilePanelInProject("Save GroundTileData", "New GroundTileData", "Asset", "Save GroundTileData", "Assets/Create/Tiles");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<GroundTileData>(), path);
    }
}
