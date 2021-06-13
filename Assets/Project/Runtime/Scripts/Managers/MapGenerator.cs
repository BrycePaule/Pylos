using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
	public MapSettings MapSettings;

	[Header("Colour Shifts")]
	[Range(0, 1f)] public float TileHueChangeStrength;
	[Range(0, 1f)] public float TileSaturationChangeStrength;
	[Range(0, 1f)] public float TileVibranceChangeStrength;

	[Header("Objects")]
	[SerializeField] private Tilemap tilemap;
	[SerializeField] private GameObject treeContainer;
	[SerializeField] private GameObject shrubContainer;
	[SerializeField] private GameObject sandSpotContainer;
	[SerializeField] private GameObject dirtSpotContainer;
	[SerializeField] private GameObject stoneContainer;

	[Header("Prefabs")]
	[SerializeField] private GameObject treePrefab;
	[SerializeField] private GameObject stonePrefab;
	[SerializeField] private List<GameObject> shrubPrefabs;
	[SerializeField] private List<GameObject> sandSpotsPrefabs;
	[SerializeField] private List<GameObject> dirtSpotsPrefabs;

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

		GenerateTileMap(GenerateTexture(seed));
	}

	private Texture2D GenerateTexture(float seed)
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

	private void GenerateTileMap(Texture2D noiseMap)
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

					if (RandomChance.Roll(MapSettings.SandSpotSpawnPercent))
					{
						InstantiateObject(sandSpotsPrefabs[Random.Range(0, sandSpotsPrefabs.Count)], pos, tileData, sandSpotContainer);
					}
				}
				else if (height > MapSettings.SandMaxHeight && height <=  MapSettings.DirtMaxHeight)
				{
					tileData = SetTileData(tileData, height);
					tilemap.SetTile(pos, tileData.Tile);

					if (RandomChance.Roll(MapSettings.DirtSpotSpawnPercent))
					{
						InstantiateObject(dirtSpotsPrefabs[Random.Range(0, dirtSpotsPrefabs.Count)], pos, tileData, dirtSpotContainer);
					}
				}
				else if (height > MapSettings.DirtMaxHeight && height <= MapSettings.GrassMaxHeight)
				{
					tileData = SetTileData(tileData, height);
					tilemap.SetTile(pos, tileData.Tile);

					if (RandomChance.Roll(MapSettings.TreeSpawnPercent))
					{
						GameObject tree = InstantiateObject(treePrefab, pos, tileData, treeContainer);

						tileData.TravelType.Remove(TileTravelType.Walkable);
						tree.GetComponent<Container>().TileLoc = new Vector2Int(pos.x, pos.y);
						tree.GetComponent<ExhaustableContainer>().Put(ItemID.Wood, 10);
					}
					else if (RandomChance.Roll(MapSettings.ShrubSpawnPercent))
					{
						InstantiateObject(shrubPrefabs[Random.Range(0, shrubPrefabs.Count)], pos, tileData, shrubContainer);
					}
				}
				else if (height > MapSettings.GrassMaxHeight)
				{
					tileData = SetTileData(tileData, height);
					tilemap.SetTile(pos, tileData.Tile);

					if (RandomChance.Roll(MapSettings.StoneSpawnPercent))
					{
						GameObject stone = InstantiateObject(stonePrefab, pos, tileData, stoneContainer);

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

	private GameObject InstantiateObject(GameObject prefab, Vector3 pos, GroundTileData tileData, GameObject container)
	{
		GameObject obj = Instantiate(prefab, new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0), Quaternion.identity, container.transform);

		obj.name = prefab.name;
		obj.GetComponent<SpriteRenderer>().flipX = RandomChance.Roll(50) ? true : false;

		tileData.ContainedObjects.Add(obj);

		return obj;
	}

	// UTILS
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

	public float ScaleValue(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
	{
		float OldRange = (OldMax - OldMin);
		float NewRange = (NewMax - NewMin);
		return (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
	}
}
