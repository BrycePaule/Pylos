using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class MapGenerator : MonoBehaviour
{
	[Header("References")]
	public SettingsInjecter SettingsInjecter;
	public ColourPalette ColourPalette;
	public Tilemap Tilemap;

	[Header("Container References")]
	public GameObject TreeContainer;
	public GameObject StoneContainer;
	public GameObject ShrubContainer;
	public GameObject SandSpotContainer;
	public GameObject DirtSpotContainer;

	[Header("Colour Shift Settings")]
	[Range(0, 1f)] public float TileHueChangeStrength;
	[Range(0, 1f)] public float TileSaturationChangeStrength;
	[Range(0, 1f)] public float TileVibranceChangeStrength;

	[Header("Prefabs")]
	public GameObject TreePrefab;
	public GameObject StonePrefab;
	public GameObject ClutterPrefab;

	[Header("Sprites")]
	public List<Sprite> TreeSprites;
	public List<Sprite> StoneSprites;
	public List<Sprite> ShrubSprites;
	public List<Sprite> SandSpotSprites;
	public List<Sprite> DirtSpotSprites;

	[Header("Tiles")]
	public GroundTileData GroundTileData;

	[Header("Generation")]
	public Texture2D NoiseTexture;

	private float[,] terrainMap;
	private float[,] biomeMap;

	private TerrainGenerator terrainGenerator;

	private void Awake()
	{
		terrainGenerator = GetComponent<TerrainGenerator>();
		CreateMap();
	}

	public void CreateMap()
	{
		SettingsInjecter.MapSettings.Seed = SettingsInjecter.MapSettings.Seed == 0f ? Random.Range(0f, 1f) : SettingsInjecter.MapSettings.Seed;
		print("Seed: " + SettingsInjecter.MapSettings.Seed);

		SettingsInjecter.MapSettings.Tiles = new GroundTileData[SettingsInjecter.MapSettings.MapSize, SettingsInjecter.MapSettings.MapSize];

		terrainMap = terrainGenerator.GenerateHeightMap(SettingsInjecter.MapSettings.TerrainNoiseSettings);
		biomeMap = terrainGenerator.GenerateHeightMap(SettingsInjecter.MapSettings.BiomeNoiseSettings);
		PopulateTilemap();

		Renderer renderer = GetComponent<Renderer>();
		renderer.material.mainTexture = terrainGenerator.GenerateTextureBlend(terrainMap, biomeMap, ColourPalette);

	}

	private float[,] GetHeightMap(float seed, List<NoiseFreqAmp> noiseSettings)
	{
		return terrainGenerator.GenerateHeightMap(noiseSettings);
	}

	private void PopulateTilemap()
	{
		for (int y = 0; y < SettingsInjecter.MapSettings.MapSize; y++)
		{
			for (int x = 0; x < SettingsInjecter.MapSettings.MapSize; x++)
			{
				Vector3Int pos = new Vector3Int(x, y, 1);
				float terrainHeight = terrainMap[x, y];

				GroundTileData tileData = Instantiate(GroundTileData);

				// water
				if (terrainHeight <= SettingsInjecter.MapSettings.WaterMaxHeight)
				{
					tileData = SetTileData(tileData, terrainGenerator.GetTerrainColour(ColourPalette, terrainMap[x, y], biomeMap[x, y]), terrainHeight, walkable: false, swimmable: true);
					Tilemap.SetTile(pos, tileData.Tile);
				}
				
				// sand
				else if (terrainHeight > SettingsInjecter.MapSettings.WaterMaxHeight && terrainHeight <= SettingsInjecter.MapSettings.SandMaxHeight )
				{
					tileData = SetTileData(tileData, terrainGenerator.GetTerrainColour(ColourPalette, terrainMap[x, y], biomeMap[x, y]), terrainHeight);
					Tilemap.SetTile(pos, tileData.Tile);

					if (RandomChance.Roll(SettingsInjecter.MapSettings.SandSpotSpawnPercent))
					{
						InstantiateObject(ClutterPrefab, pos, "Sand", tileData, SandSpotContainer, SandSpotSprites);
					}
				}

				// dirt
				else if (terrainHeight > SettingsInjecter.MapSettings.SandMaxHeight && terrainHeight <=  SettingsInjecter.MapSettings.DirtMaxHeight)
				{
					tileData = SetTileData(tileData, terrainGenerator.GetTerrainColour(ColourPalette, terrainMap[x, y], biomeMap[x, y]), terrainHeight);
					Tilemap.SetTile(pos, tileData.Tile);

					if (RandomChance.Roll(SettingsInjecter.MapSettings.DirtSpotSpawnPercent))
					{
						InstantiateObject(ClutterPrefab, pos, "Dirt", tileData, DirtSpotContainer, DirtSpotSprites);
					}
				}

				// grass
				else if (terrainHeight > SettingsInjecter.MapSettings.DirtMaxHeight && terrainHeight <= SettingsInjecter.MapSettings.GrassMaxHeight)
				{
					tileData = SetTileData(tileData, terrainGenerator.GetTerrainColour(ColourPalette, terrainMap[x, y], biomeMap[x, y]), terrainHeight);
					Tilemap.SetTile(pos, tileData.Tile);

					if (RandomChance.Roll(SettingsInjecter.MapSettings.TreeSpawnPercent))
					{
						GameObject tree = InstantiateObject(TreePrefab, pos, "Tree", tileData, TreeContainer, TreeSprites, flipY: false);

						tileData.TravelType.Add(TileTravelType.Impassable);
						tree.GetComponent<Container>().TileLoc = new Vector2Int(pos.x, pos.y);
						tree.GetComponent<ExhaustableContainer>().Put(1, 10);
					}
					else if (RandomChance.Roll(SettingsInjecter.MapSettings.ShrubSpawnPercent))
					{
						InstantiateObject(ClutterPrefab, pos, "Shrub", tileData, ShrubContainer, ShrubSprites);
					}
				}

				// stone
				else if (terrainHeight > SettingsInjecter.MapSettings.GrassMaxHeight)
				{
					tileData = SetTileData(tileData, terrainGenerator.GetTerrainColour(ColourPalette, terrainMap[x, y], biomeMap[x, y]), terrainHeight);
					Tilemap.SetTile(pos, tileData.Tile);

					if (RandomChance.Roll(SettingsInjecter.MapSettings.StoneSpawnPercent))
					{
						GameObject stone = InstantiateObject(StonePrefab, pos, "Stone", tileData, StoneContainer, StoneSprites, flipY: false);

						stone.GetComponent<Container>().TileLoc = new Vector2Int(pos.x, pos.y);
						stone.GetComponent<ExhaustableContainer>().Put(2, 10);
					}
				}
				SettingsInjecter.MapSettings.Tiles[x, y] = tileData;
			}
		}
	}

	private GroundTileData SetTileData(GroundTileData tileData, Color baseColour, float height, bool walkable = true, bool swimmable = false)
	{
		tileData.GroundType = GetGroundTypeByHeight(height);
		tileData.Tile.color = Colors.AlterColour(baseColour, satChange: TileSaturationChangeStrength);
		
		if (walkable) { tileData.TravelType.Add(TileTravelType.Walkable); } 
		if (swimmable) { tileData.TravelType.Add(TileTravelType.Swimmable); } 

		return tileData;
	}

	private GameObject InstantiateObject(GameObject prefab, Vector3 pos, string name, GroundTileData tileData, GameObject container, List<Sprite> sprites,  bool flipX = true, bool flipY = true)
	{
		GameObject obj = Instantiate(prefab, new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0), Quaternion.identity, container.transform);

		obj.name = name;

		SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
		sr.sprite = sprites[Random.Range(0, sprites.Count)];
		if (flipX)
			sr.flipX = RandomChance.Roll(50) ? true : false;
		if (flipY)
			sr.flipY = RandomChance.Roll(50) ? true : false;

		tileData.ContainedObjects.Add(obj);

		return obj;
	}

	// UTILS
	private GroundType GetGroundTypeByHeight(float height)
	{
		if (height <= SettingsInjecter.MapSettings.WaterMaxHeight)
			return GroundType.Water;
		else if (height > SettingsInjecter.MapSettings.WaterMaxHeight && height <= SettingsInjecter.MapSettings.SandMaxHeight)
			return GroundType.Sand;
		else if (height > SettingsInjecter.MapSettings.SandMaxHeight && height <=  SettingsInjecter.MapSettings.DirtMaxHeight)
			return GroundType.Dirt;
		else if (height > SettingsInjecter.MapSettings.DirtMaxHeight && height <= SettingsInjecter.MapSettings.GrassMaxHeight)
			return GroundType.Grass;
		else
			return GroundType.Stone;
	}
}
