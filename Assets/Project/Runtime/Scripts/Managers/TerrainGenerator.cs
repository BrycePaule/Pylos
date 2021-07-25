using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
	[Header("References")]
	public SettingsInjecter SettingsInjecter;

	// HeightMap
	public float[,] GenerateHeightMap(List<NoiseFreqAmp> settings)
	{
		float[,] heightMap = new float[SettingsInjecter.MapSettings.MapSize, SettingsInjecter.MapSettings.MapSize];

		for (int y = 0; y < SettingsInjecter.MapSettings.MapSize; y++)
		{
			for (int x = 0; x < SettingsInjecter.MapSettings.MapSize; x++)
			{
				float height = 0;
				foreach (NoiseFreqAmp FA in settings)
				{
					height += Mathf.Clamp01(PerlinSample(x, y, FA.Frequency, FA.Amplitude));
				}

				heightMap[x, y] = height;
			}
		}

		return heightMap;
	}

	private float PerlinSample(int x, int y, float freq, float amp)
	{
		float xCoord = (float) x / SettingsInjecter.MapSettings.MapSize * SettingsInjecter.MapSettings.Scale + SettingsInjecter.MapSettings.OffsetX;
		float yCoord = (float) y / SettingsInjecter.MapSettings.MapSize * SettingsInjecter.MapSettings.Scale + SettingsInjecter.MapSettings.OffsetY;

		return Mathf.PerlinNoise((xCoord + SettingsInjecter.MapSettings.MapSize * SettingsInjecter.MapSettings.Seed) * freq, (yCoord + SettingsInjecter.MapSettings.MapSize * SettingsInjecter.MapSettings.Seed) * freq) * amp;
	}

	//	Textures
	public Texture2D GenerateTexture(float[,] heightMap, ColourPalette palette, bool greyscale = false)
	{
		Texture2D tex = new Texture2D(SettingsInjecter.MapSettings.MapSize, SettingsInjecter.MapSettings.MapSize);

		for (int y = 0; y < SettingsInjecter.MapSettings.MapSize; y++)
		{
			for (int x = 0; x < SettingsInjecter.MapSettings.MapSize; x++)
			{
				if (greyscale)
				{
					tex.SetPixel(x, y, GetTerrainColour(palette, heightMap[x, y], heightMap[x, y], greyscale: greyscale));
				}
				else
				{
					tex.SetPixel(x, y, GetTerrainColour(palette, heightMap[x, y], 0));
				}
			}
		}

		tex.Apply();
		return tex;
	}

	public Texture2D GenerateTextureBlend(float[,] terrainMap, float[,] biomeMap, ColourPalette palette)
	{
		Texture2D tex = new Texture2D(SettingsInjecter.MapSettings.MapSize, SettingsInjecter.MapSettings.MapSize);

		for (int y = 0; y < SettingsInjecter.MapSettings.MapSize; y++)
		{
			for (int x = 0; x < SettingsInjecter.MapSettings.MapSize; x++)
			{
				tex.SetPixel(x, y, GetTerrainColour(palette, terrainMap[x, y], biomeMap[x, y]));
			}
		}

		tex.Apply();
		return tex;
	}

	// Colours
	public Color GetTerrainColour(ColourPalette palette, float terrainHeight, float biomeHeight, bool greyscale = false)
	{
		if (terrainHeight <= SettingsInjecter.MapSettings.WaterMaxHeight)
		{
			if (greyscale) { return Color.white; }
			return palette.Water;
		}
		else if (terrainHeight > SettingsInjecter.MapSettings.WaterMaxHeight && terrainHeight <= SettingsInjecter.MapSettings.SandMaxHeight )
		{
			if (greyscale) { return Color.white; }
			return palette.Sand;
		}
		else if (terrainHeight > SettingsInjecter.MapSettings.SandMaxHeight && terrainHeight <=  SettingsInjecter.MapSettings.DirtMaxHeight)
		{
			Color colour;
			if (greyscale)
			{ 
				colour =  Color.white; 
			}
			else
			{
				colour =  palette.Dirt; 
			}

			colour = DarkenColourByBiomeHeight(biomeHeight, colour); 

			return colour;
		}
		else if (terrainHeight > SettingsInjecter.MapSettings.DirtMaxHeight && terrainHeight <= SettingsInjecter.MapSettings.GrassMaxHeight)
		{
			Color colour;
			if (greyscale)
			{ 
				colour =  Color.white; 
			}
			else
			{
				colour =  palette.Grass; 
			}

			colour = DarkenColourByBiomeHeight(biomeHeight, colour); 

			return colour;
		}
		else if (terrainHeight > SettingsInjecter.MapSettings.GrassMaxHeight)
		{
			if (greyscale) { return Color.white; }
			return palette.Stone;
		}

		return Color.black;
	}

	private Color DarkenColourByBiomeHeight(float height, Color colour)
	{
		Color baseColour = colour;
		float[] biomeHeightSteps = GetBiomeColourSteps(SettingsInjecter.MapSettings.BiomeSteps);

		for (int i = 0; i < biomeHeightSteps.Length; i++)
		{
			if (height >= biomeHeightSteps[i])
				colour = Colors.Darken(baseColour, SettingsInjecter.MapSettings.BiomeColourDiff * i);
		}
		
		return colour;
	}

	private float[] GetBiomeColourSteps(int steps, float lower = 0f, float upper = 1.01f)
	{
		float[] biomeHeightSteps = new float[steps];

		for (int i = 0; i < steps; i++)
		{
			if (lower == 0f)
			{
				biomeHeightSteps[i] = (upper / (steps + 1)) * (i + 1);
			}
			else
			{
				biomeHeightSteps[i] = lower + (((upper / lower) / (steps + 1)) * (i + 1));
			}
		}

		return biomeHeightSteps;
	}
}

