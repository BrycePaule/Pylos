using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
	[Header("Settings")]
	public int MapSize;

	[Header("Perlin Noise Generation")]
	public float Scale;
	public float OffsetX;
	public float OffsetY;
	[Range(0, 1f)]
	public float WaterMaxHeight;
	[Range(0, 1f)]
	public float SandMaxHeight;
	[Range(0, 1f)]
	public float DirtMaxHeight;
	[Range(0, 1f)]
	public float GrassMaxHeight;

	[Header("Tile Settings")]
	public Color WaterColour;
	public Color SandColour;
	public Color DirtColour;
	public Color GrassColour;
	public Color StoneColour;
	[Range(0, 1f)]
	public float TileHueChangeStrength;
	[Range(0, 1f)]
	public float TileSaturationChangeStrength;
	[Range(0, 1f)]
	public float TileVibranceChangeStrength;

	[Header("Objects")]
	[SerializeField] private Tilemap tilemap;
	[SerializeField] private GameObject treeContainer;
	[SerializeField] private MapManager mapManager;
	[SerializeField] private NPCGenerator mobSpawner;

	[Header("Prefabs")]
	[SerializeField] private GameObject treePrefab;
	[SerializeField] private GameObject stonePrefab;

	[Header("Tiles")]
	[SerializeField] private Tile baseTile;
	[SerializeField] private GroundTileData groundTileDataAsset;

	private void Awake()
	{
		float seed = Random.Range(0, 1f);

		Renderer renderer = GetComponent<Renderer>();
		renderer.material.mainTexture = GenerateTexture(seed);

		SetTileMap(GenerateTexture(seed));
		print("Seed: " + seed);
	}

	public Texture2D GenerateTexture(float seed)
	{
		Texture2D texture = new Texture2D(MapSize, MapSize);

		for (int y = 0; y < MapSize; y++)
		{
			for (int x = 0; x < MapSize; x++)
			{
				float xCoord = (float) x / MapSize * Scale + OffsetX;
				float yCoord = (float) y / MapSize * Scale + OffsetY;
				
				float sample = Mathf.PerlinNoise(xCoord + MapSize * seed, yCoord + MapSize * seed);
				
				Color colour = new Color(sample, sample, sample);
				texture.SetPixel(x, y, colour);    
			}   
		}

		texture.Apply();
		return texture;
	}

	private void SetTileMap(Texture2D noiseMap)
	{
		for (int y = 0; y < MapSize; y++)
		{
			for (int x = 0; x < MapSize; x++)
			{
				Vector3Int pos = new Vector3Int(x, y, 0);
				float height = noiseMap.GetPixel(x, y).r;

				GroundTileData tileData = Instantiate(groundTileDataAsset);
				mapManager.Tiles[x, y] = tileData;

				if (height <= WaterMaxHeight)
				{
					tileData = SetTileData(tileData, height, walkable: false, swimmable: true);
					tilemap.SetTile(pos, tileData.Tile);
				}
				else if (height > WaterMaxHeight && height <= SandMaxHeight )
				{
					tileData = SetTileData(tileData, height);
					tilemap.SetTile(pos, tileData.Tile);
				}
				else if (height > SandMaxHeight && height <=  DirtMaxHeight)
				{
					tileData = SetTileData(tileData, height);
					tilemap.SetTile(pos, tileData.Tile);
				}
				else if (height > DirtMaxHeight && height <= GrassMaxHeight)
				{
					tileData = SetTileData(tileData, height);
					tilemap.SetTile(pos, tileData.Tile);

					if (RandomChance.Roll(5))
					{
						GameObject tree = Instantiate(treePrefab, new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.identity, treeContainer.transform);
						tree.name = treePrefab.name;
						tileData.IsWalkable = false;
						tileData.ContainedObjects.Add(tree);

						tree.GetComponent<ExhaustableContainer>().Put(ItemID.Wood, 10);
					}
				}
				else if (height > GrassMaxHeight)
				{
					tileData = SetTileData(tileData, height);
					tilemap.SetTile(pos, tileData.Tile);

					if (RandomChance.Roll(10))
					{
						GameObject stone = Instantiate(stonePrefab, new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.identity, treeContainer.transform);
						stone.name = stonePrefab.name;
						tileData.ContainedObjects.Add(stone);
					}
				}
			}
		}
	}

	private GroundTileData SetTileData(GroundTileData tileData, float height, bool walkable = true, bool swimmable = false)
	{
		tileData.GroundType = GetTileByHeight(height);
		tileData.Tile.color = Colors.AlterColour(tileData.ColorLookup(tileData.GroundType), satChange: TileSaturationChangeStrength);
		tileData.IsWalkable = walkable;
		tileData.IsSwimmable = swimmable;

		return tileData;
	}

	public float ScaleValue(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
	{
		float OldRange = (OldMax - OldMin);
		float NewRange = (NewMax - NewMin);
		return (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
	}

	public int RandomIntInBounds()
	{
		return (int) Random.Range(0, MapSize);
	}

	private GroundType GetTileByHeight(float height)
	{
		if (height <= WaterMaxHeight)
		{
			return GroundType.Water;
		}
		else if (height > WaterMaxHeight && height <= SandMaxHeight)
		{
			return GroundType.Sand;
		}
		else if (height > SandMaxHeight && height <=  DirtMaxHeight)
		{
			return GroundType.Dirt;
		}
		else if (height > DirtMaxHeight && height <= GrassMaxHeight)
		{
			return GroundType.Grass;
		}
		else
		{
			return GroundType.Stone;
		}
	}
}
