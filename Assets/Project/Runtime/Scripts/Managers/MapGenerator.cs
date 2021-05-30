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
    [SerializeField] private Tile[] tilesTypes;


    private void Awake()
    {
        float seed = Random.Range(0, 1f);

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture(seed);

        SetTileMap(GenerateTexture(seed));
        print("Seed: " + seed);
    }

    private void Start() 
    {
        // mobSpawner.SpawnMobs(10);
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
                float value = noiseMap.GetPixel(x, y).r;

                GroundTileData GroundTileData = Instantiate(groundTileDataAsset);
                mapManager.Tiles[x, y] = GroundTileData;

                if (value <= WaterMaxHeight)
                {
                    GroundTileData.GroundType = GroundType.Water;
                    GroundTileData.Tile.color = WaterColour;
                    GroundTileData.Tile.color = AlterColour(GroundTileData.Tile.color, ScaleValue(0, WaterMaxHeight, -1, 0, value));
                    GroundTileData.IsWalkable = false;
                    GroundTileData.IsSwimmable = true;
                    tilemap.SetTile(pos, GroundTileData.Tile);
                }
                else if (value > WaterMaxHeight && value <= SandMaxHeight )
                {
                    GroundTileData.GroundType = GroundType.Sand;
                    GroundTileData.Tile.color = SandColour;
                    GroundTileData.Tile.color = AlterColour(GroundTileData.Tile.color, ScaleValue(WaterMaxHeight, SandMaxHeight, -1, 0, value));
                    tilemap.SetTile(pos, GroundTileData.Tile);
                }
                else if (value > SandMaxHeight && value <=  DirtMaxHeight)
                {
                    GroundTileData.GroundType = GroundType.Dirt;
                    GroundTileData.Tile.color = DirtColour;
                    GroundTileData.Tile.color = AlterColour(GroundTileData.Tile.color, ScaleValue(SandMaxHeight, DirtMaxHeight, -1, 0, value), invert: true);
                    tilemap.SetTile(pos, GroundTileData.Tile);
                }
                else if (value > DirtMaxHeight && value <= GrassMaxHeight)
                {
                    GroundTileData.GroundType = GroundType.Grass;
                    GroundTileData.Tile.color = GrassColour;
                    GroundTileData.Tile.color = AlterColour(GroundTileData.Tile.color, ScaleValue(DirtMaxHeight, 1, -1, 0, value));
                    tilemap.SetTile(pos, GroundTileData.Tile);

                    int roll = Random.Range(0, 100);
                    if (roll < 5)
                    {
                        GameObject tree = Instantiate(treePrefab, new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.identity, treeContainer.transform);
                        GroundTileData.IsWalkable = false;
                        GroundTileData.ContainedObjects.Add(tree);
                    }
                }
                else if (value > GrassMaxHeight)
                {
                    GroundTileData.GroundType = GroundType.Stone;
                    GroundTileData.Tile.color = StoneColour;
                    GroundTileData.Tile.color = AlterColour(GroundTileData.Tile.color, ScaleValue(GrassMaxHeight, 1, -1, 0, value));
                    tilemap.SetTile(pos, GroundTileData.Tile);

                    int roll = Random.Range(0, 100);
                    if (roll < 10)
                    {
                        GameObject stone = Instantiate(stonePrefab, new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.identity, treeContainer.transform);
                        GroundTileData.ContainedObjects.Add(stone);
                    }
                }
            }
        }
    }

    private Color RandomiseColour(Color colour)
    {
        float h, s, v;
        Color.RGBToHSV(colour, out h, out s, out v);
        return Color.HSVToRGB(
            h + Random.Range(h * -TileHueChangeStrength, h * TileHueChangeStrength), 
            s + Random.Range(s * -TileSaturationChangeStrength, s * TileSaturationChangeStrength),
            v + Random.Range(v * -TileVibranceChangeStrength, v * TileVibranceChangeStrength)
        );
    }

    private Color AlterColour(Color colour, float heightOffset, bool invert = false)
    {
        float h, s, v;
        Color.RGBToHSV(colour, out h, out s, out v);

        int inversion = (invert == true) ? -1 : 1;

        float Hdheight = TileHueChangeStrength * heightOffset;
        float Sdheight = TileSaturationChangeStrength * heightOffset;
        float Vdheight = TileVibranceChangeStrength * heightOffset;

        float Hdrandom = h * Random.Range(-0.02f, 0.02f);
        float Sdrandom = s * Random.Range(-0.02f, 0.02f);
        float Vdrandom = v * Random.Range(-0.02f, 0.02f);

        return Color.HSVToRGB(
            h + ((Hdheight) * inversion),
            s + ((Sdheight + Sdrandom) * inversion),
            v + ((Vdheight + Vdrandom) * inversion)
        );
    }

    private Tile SelectRandomTile()
    {
        return tilesTypes[Random.Range(0, tilesTypes.Length)];
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
}
