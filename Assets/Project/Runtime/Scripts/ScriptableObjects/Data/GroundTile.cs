using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[SerializeField]
[CreateAssetMenu(menuName = "Tiles/Ground Tile")]
public class GroundTile : TileBase
{
	public Tile Tile;
	public List<TileTravelType> TravelTypes;
	public List<GameObject> ContainedObjects;
}