using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
	public MapSettings MapSettings;

	[Range(0, 1f)] public float TileHueChangeStrength;
	[Range(0, 1f)] public float TileSaturationChangeStrength;
	[Range(0, 1f)] public float TileVibranceChangeStrength;

	[Header("Objects")]
	[SerializeField] private Tilemap tilemap;
	[SerializeField] private GameObject treeContainer;
	[SerializeField] private GameObject shrubContainer;
	[SerializeField] private GameObject stoneContainer;

	[Header("Prefabs")]
	[SerializeField] private GameObject treePrefab;
	[SerializeField] private GameObject stonePrefab;
	[SerializeField] private List<GameObject> shrubPrefabs;

	[Header("Tiles")]
	[SerializeField] private Tile baseTile;
	[SerializeField] private GroundTileData groundTileDataAsset;

	private void Awake()
	{
		float seed = MapSettings.Seed == 0f ? Random.Range(0f, 1f) : MapSettings.Seed;
		print("Seed: " + seed);

		MapSettings.Tiles = new GroundTileData[MapSettings.MapSize, MapSettings.MapSize];
		Renderer renderer = GetComponent<Renderer>();
		renderer.material.mainTexture = GenerateTexture(seed);

		SetTileMap(GenerateTexture(seed));
	}

	public Texture2D GenerateTexture(float seed)
	{
		Texture2D texture = new Texture2D(MapSettings.MapSize, MapSettings.MapSize);

		for (int y = 0; y < MapSettings.MapSize; y++)
		{
			for (int x = 0; x < MapSettings.MapSize; x++)
			{
				float xCoord = (float) x / MapSettings.MapSize * MapSettings.Scale + MapSettings.OffsetX;
				float yCoord = (float) y / MapSettings.MapSize * MapSettings.Scale + MapSettings.OffsetY;
				
				float sample = Mathf.PerlinNoise(xCoord + MapSettings.MapSize * seed, yCoord + MapSettings.MapSize * seed);
				
				Color colour = new Color(sample, sample, sample);
				texture.SetPixel(x, y, colour);    
			}   
		}

		texture.Apply();
		return texture;
	}

	private void SetTileMap(Texture2D noiseMap)
	{
		for (int y = 0; y < MapSettings.MapSize; y++)
		{
			for (int x = 0; x < MapSettings.MapSize; x++)
			{
				Vector3Int pos = new Vector3Int(x, y, 0);
				float height = noiseMap.GetPixel(x, y).r;

				GroundTileData tileData = Instantiate(groundTileDataAsset);
				MapSettings.Tiles[x, y] = tileData;

				if (height <= MapSettings.WaterMaxHeight)
				{
					tileData = SetTileData(tileData, height, walkable: false, swimmable: true);
					tilemap.SetTile(pos, tileData.Tile);
				}
				else if (height > MapSettings.WaterMaxHeight && height <= MapSettings.SandMaxHeight )
				{
					tileData = SetTileData(tileData, height);
					tilemap.SetTile(pos, tileData.Tile);
				}
				else if (height > MapSettings.SandMaxHeight && height <=  MapSettings.DirtMaxHeight)
				{
					tileData = SetTileData(tileData, height);
					tilemap.SetTile(pos, tileData.Tile);
				}
				else if (height > MapSettings.DirtMaxHeight && height <= MapSettings.GrassMaxHeight)
				{
					tileData = SetTileData(tileData, height);
					tilemap.SetTile(pos, tileData.Tile);

					if (RandomChance.Roll(MapSettings.TreeSpawnPercent))
					{
						GameObject tree = Instantiate(treePrefab, new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.identity, treeContainer.transform);
						tree.name = treePrefab.name;
						tree.GetComponent<SpriteRenderer>().flipX = RandomChance.Roll(50) ? true : false;
						tileData.TravelType.Remove(TileTravelType.Walkable);
						tileData.ContainedObjects.Add(tree);

						tree.GetComponent<Container>().TileLoc = new Vector2Int(pos.x, pos.y);
						tree.GetComponent<ExhaustableContainer>().Put(ItemID.Wood, 10);
					}
					else if (RandomChance.Roll(MapSettings.ShrubSpawnPercent))
					{
						GameObject shrub = Instantiate(shrubPrefabs[Random.Range(0, shrubPrefabs.Count)], new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.identity, shrubContainer.transform);
						shrub.name = "Shrub";
						shrub.GetComponent<SpriteRenderer>().flipX = RandomChance.Roll(50) ? true : false;
						tileData.ContainedObjects.Add(shrub);
					}
				}
				else if (height > MapSettings.GrassMaxHeight)
				{
					tileData = SetTileData(tileData, height);
					tilemap.SetTile(pos, tileData.Tile);

					if (RandomChance.Roll(MapSettings.StoneSpawnPercent))
					{
						GameObject stone = Instantiate(stonePrefab, new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.identity, stoneContainer.transform);
						stone.name = stonePrefab.name;
						stone.GetComponent<SpriteRenderer>().flipX = RandomChance.Roll(50) ? true : false;
						tileData.TravelType.Remove(TileTravelType.Walkable);
						tileData.ContainedObjects.Add(stone);

						stone.GetComponent<Container>().TileLoc = new Vector2Int(pos.x, pos.y);
						stone.GetComponent<ExhaustableContainer>().Put(ItemID.Stone, 10);
					}
				}
			}
		}
	}

	private GroundTileData SetTileData(GroundTileData tileData, float height, bool walkable = true, bool swimmable = false)
	{
		tileData.GroundType = GetTileByHeight(height);
		tileData.Tile.color = Colors.AlterColour(tileData.ColorLookup(tileData.GroundType), satChange: TileSaturationChangeStrength);
		
		if (walkable) { tileData.TravelType.Add(TileTravelType.Walkable); } 
		if (swimmable) { tileData.TravelType.Add(TileTravelType.Swimmable); } 

		return tileData;
	}

	public float ScaleValue(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
	{
		float OldRange = (OldMax - OldMin);
		float NewRange = (NewMax - NewMin);
		return (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
	}

	
	private GroundType GetTileByHeight(float height)
	{
		if (height <= MapSettings.WaterMaxHeight)
			return GroundType.Water;
		else if (height > MapSettings.WaterMaxHeight && height <= MapSettings.SandMaxHeight)
			return GroundType.Sand;
		else if (height > MapSettings.SandMaxHeight && height <=  MapSettings.DirtMaxHeight)
			return GroundType.Dirt;
		else if (height > MapSettings.DirtMaxHeight && height <= MapSettings.GrassMaxHeight)
			return GroundType.Grass;
		else
			return GroundType.Stone;
	}
}
