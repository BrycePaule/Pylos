using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class MapGenerator : MonoBehaviour
{
	[Header("References")]
	public SettingsInjecter SettingsInjecter;
	public Tilemap tilemap;

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
	public Tile BaseTile;
	public GroundTileData GroundTileData;

	[Header("Generation")]
	public float Seed;
	public Texture2D NoiseTexture;

	private void Awake()
	{
		Seed = SettingsInjecter.MapSettings.Seed == 0f ? Random.Range(0f, 1f) : SettingsInjecter.MapSettings.Seed;
		print("Seed: " + Seed);

		SettingsInjecter.MapSettings.Tiles = new GroundTileData[SettingsInjecter.MapSettings.MapSize, SettingsInjecter.MapSettings.MapSize];

		GenerateTexture(Seed);
		Renderer renderer = GetComponent<Renderer>();
		renderer.material.mainTexture = NoiseTexture;

		PopulateTilemap();
	}

	private void GenerateTexture(float seed)
	{
		if (SettingsInjecter.MapSettings.UseTextureFromFile)
		{
			string filePath = Application.dataPath + "/../Assets/Project/TextureExports/" + SettingsInjecter.MapSettings.TextureName + ".png";

			if (File.Exists(filePath)) {
				print("Loaded Seed: " + SettingsInjecter.MapSettings.TextureName);
				NoiseTexture = new Texture2D(2, 2);
				NoiseTexture.LoadImage(File.ReadAllBytes(filePath));
				return;
			}

		}

		NoiseTexture = new Texture2D(SettingsInjecter.MapSettings.MapSize, SettingsInjecter.MapSettings.MapSize);

		for (int y = 0; y < SettingsInjecter.MapSettings.MapSize; y++)
		{
			for (int x = 0; x < SettingsInjecter.MapSettings.MapSize; x++)
			{
				float xCoord = (float) x / SettingsInjecter.MapSettings.MapSize * SettingsInjecter.MapSettings.Scale + SettingsInjecter.MapSettings.OffsetX;
				float yCoord = (float) y / SettingsInjecter.MapSettings.MapSize * SettingsInjecter.MapSettings.Scale + SettingsInjecter.MapSettings.OffsetY;
				
				float sample = Mathf.PerlinNoise(xCoord + SettingsInjecter.MapSettings.MapSize * seed, yCoord + SettingsInjecter.MapSettings.MapSize * seed);
				
				Color colour = new Color(sample, sample, sample);
				NoiseTexture.SetPixel(x, y, colour);    
			}
		}

		NoiseTexture.Apply();
	}

	private void PopulateTilemap()
	{
		for (int y = 0; y < SettingsInjecter.MapSettings.MapSize; y++)
		{
			for (int x = 0; x < SettingsInjecter.MapSettings.MapSize; x++)
			{
				Vector3Int pos = new Vector3Int(x, y, 0);
				float height = NoiseTexture.GetPixel(x, y).r;

				GroundTileData tileData = Instantiate(GroundTileData);
				SettingsInjecter.MapSettings.Tiles[x, y] = tileData;

				if (height <= SettingsInjecter.MapSettings.WaterMaxHeight)
				{
					tileData = SetTileData(tileData, height, walkable: false, swimmable: true);
					tilemap.SetTile(pos, tileData.Tile);
				}
				else if (height > SettingsInjecter.MapSettings.WaterMaxHeight && height <= SettingsInjecter.MapSettings.SandMaxHeight )
				{
					tileData = SetTileData(tileData, height);
					tilemap.SetTile(pos, tileData.Tile);

					if (RandomChance.Roll(SettingsInjecter.MapSettings.SandSpotSpawnPercent))
					{
						InstantiateObject(ClutterPrefab, pos, "Sand", tileData, SandSpotContainer, SandSpotSprites);
					}
				}
				else if (height > SettingsInjecter.MapSettings.SandMaxHeight && height <=  SettingsInjecter.MapSettings.DirtMaxHeight)
				{
					tileData = SetTileData(tileData, height);
					tilemap.SetTile(pos, tileData.Tile);

					if (RandomChance.Roll(SettingsInjecter.MapSettings.DirtSpotSpawnPercent))
					{
						InstantiateObject(ClutterPrefab, pos, "Dirt", tileData, DirtSpotContainer, DirtSpotSprites);
					}
				}
				else if (height > SettingsInjecter.MapSettings.DirtMaxHeight && height <= SettingsInjecter.MapSettings.GrassMaxHeight)
				{
					tileData = SetTileData(tileData, height);
					tilemap.SetTile(pos, tileData.Tile);

					if (RandomChance.Roll(SettingsInjecter.MapSettings.TreeSpawnPercent))
					{
						GameObject tree = InstantiateObject(TreePrefab, pos, "Tree", tileData, TreeContainer, TreeSprites, flipY: false);

						tileData.TravelType.Add(TileTravelType.Impassable);
						tree.GetComponent<Container>().TileLoc = new Vector2Int(pos.x, pos.y);
						tree.GetComponent<ExhaustableContainer>().Put(ItemID.Wood, 10);
					}
					else if (RandomChance.Roll(SettingsInjecter.MapSettings.ShrubSpawnPercent))
					{
						InstantiateObject(ClutterPrefab, pos, "Shrub", tileData, ShrubContainer, ShrubSprites);
					}
				}
				else if (height > SettingsInjecter.MapSettings.GrassMaxHeight)
				{
					tileData = SetTileData(tileData, height);
					tilemap.SetTile(pos, tileData.Tile);

					if (RandomChance.Roll(SettingsInjecter.MapSettings.StoneSpawnPercent))
					{
						GameObject stone = InstantiateObject(StonePrefab, pos, "Stone", tileData, StoneContainer, StoneSprites, flipY: false);

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
	private GroundType GetTileByHeight(float height)
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

	public float ScaleValue(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
	{
		float OldRange = (OldMax - OldMin);
		float NewRange = (NewMax - NewMin);
		return (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
	}

}
