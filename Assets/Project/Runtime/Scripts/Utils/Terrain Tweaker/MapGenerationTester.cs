using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class MapGenerationTester : MonoBehaviour
{

	[System.Serializable]
	public class FreqAmpCombo
	{
		public float freq;
		public float amp;

		public FreqAmpCombo(float _freq, float _amp)
		{
			freq = _freq;
			amp = _amp;
		}
	}

	[Header("References")]
	public MapSettings MapSettings;
	public ColourPalette ColourPalette;
	public SpriteRenderer SR;

	[Header("Settings")]
	public List<FreqAmpCombo> combos;

	private float seed;
	private int mapSize;
	public Texture2D noiseTexture;

	private void Awake()
	{
		mapSize = MapSettings.MapSize;
	}

	private void Update()
	{
		seed = MapSettings.Seed;
		noiseTexture = new Texture2D(mapSize, mapSize);

		float[] heights = new float[combos.Count];

		for (int y = 0; y < mapSize; y++)
		{
			for (int x = 0; x < mapSize; x++)
			{
				for (int i = 0; i < combos.Count; i++)
				{
					heights[i] = PerlinSample(x, y, combos[i].freq, combos[i].amp);
				}

				float heightSum = 0;
				foreach (float height in heights)
				{
					heightSum += height;
				}

				Color colour = SetColor(heightSum);
				// colour = Colors.AlterColour(colour, hueChange: 0.1f);

				noiseTexture.SetPixel(x, y, colour);    
			}
		}

		noiseTexture.Apply();
		SR.material.mainTexture = noiseTexture;
	}

	private float PerlinSample(int x, int y, float f, float a)
	{
		float xCoord = (float) x / mapSize * MapSettings.Scale + MapSettings.OffsetX;
		float yCoord = (float) y / mapSize * MapSettings.Scale + MapSettings.OffsetY;
		return Mathf.PerlinNoise((xCoord + mapSize * seed) * f, (yCoord + mapSize * seed) * f) * a;
	}
	
	private Color SetColor(float height)
	{
		if (height <= MapSettings.WaterMaxHeight)
		{
			return ColourPalette.Water;
		}
		else if (height > MapSettings.WaterMaxHeight && height <= MapSettings.SandMaxHeight )
		{
			return ColourPalette.Sand;
		}
		else if (height > MapSettings.SandMaxHeight && height <=  MapSettings.DirtMaxHeight)
		{
			return ColourPalette.Dirt;
		}
		else if (height > MapSettings.DirtMaxHeight && height <= MapSettings.GrassMaxHeight)
		{
			return ColourPalette.Grass;
		}
		else if (height > MapSettings.GrassMaxHeight)
		{
			return ColourPalette.Stone;
		}

		return Color.black;
	}
}
