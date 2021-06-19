using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
	[Header("References")]
	public SettingsInjecter SettingsInjecter;
	public ColourPalette ColourPalette;

	[Header("Settings")]
	public int BiomeSteps;
	public float BiomeColourDiff;
	public List<NoiseFreqAmp> TerrainNoiseSettings;
	public List<NoiseFreqAmp> BiomeNoiseSettings;

	private float[] biomeHeightSteps;

	private Texture2D mapTexture;
	private Texture2D terrainTexture;
	private Texture2D biomeTexture;

	private void Awake()
	{
		mapTexture = new Texture2D(SettingsInjecter.MapSettings.MapSize, SettingsInjecter.MapSettings.MapSize);
		terrainTexture = new Texture2D(SettingsInjecter.MapSettings.MapSize, SettingsInjecter.MapSettings.MapSize);
		biomeTexture = new Texture2D(SettingsInjecter.MapSettings.MapSize, SettingsInjecter.MapSettings.MapSize);
	}

	public Texture2D GenerateWorldTexture()
	{
		float[] terrainHeights = new float[TerrainNoiseSettings.Count];
		float[] biomeHeights = new float[BiomeNoiseSettings.Count];

		SetBiomeHeightSteps(0f, 1.01f, BiomeSteps);

		for (int y = 0; y < SettingsInjecter.MapSettings.MapSize; y++)
		{
			for (int x = 0; x < SettingsInjecter.MapSettings.MapSize; x++)
			{
				for (int i = 0; i < Mathf.Max(TerrainNoiseSettings.Count, BiomeNoiseSettings.Count); i++)
				{
					if (i < TerrainNoiseSettings.Count)
						terrainHeights[i] = Mathf.Clamp01(PerlinSample(x, y, TerrainNoiseSettings[i].Frequency, TerrainNoiseSettings[i].Amplitude));

					if (i < BiomeNoiseSettings.Count)
						biomeHeights[i] = Mathf.Clamp01(PerlinSample(x, y, BiomeNoiseSettings[i].Frequency, BiomeNoiseSettings[i].Amplitude));
				}

				float terrainHeight = 0;
				foreach (float height in terrainHeights) { terrainHeight += height; }

				float biomeHeight = 0;
				foreach (float height in biomeHeights) { biomeHeight += height; }

				mapTexture.SetPixel(x, y, GetTerrainColour(terrainHeight, biomeHeight, blend: true));
			}
		}

		mapTexture.Apply();

		return mapTexture;
	}

	public Texture2D GenerateBiomeTexture()
	{
		float[] biomeHeights = new float[BiomeNoiseSettings.Count];

		SetBiomeHeightSteps(0f, 1.01f, BiomeSteps);

		for (int y = 0; y < SettingsInjecter.MapSettings.MapSize; y++)
		{
			for (int x = 0; x < SettingsInjecter.MapSettings.MapSize; x++)
			{
				for (int i = 0; i < Mathf.Max(TerrainNoiseSettings.Count, BiomeNoiseSettings.Count); i++)
				{
					if (i < BiomeNoiseSettings.Count)
						biomeHeights[i] = Mathf.Clamp01(PerlinSample(x, y, BiomeNoiseSettings[i].Frequency, BiomeNoiseSettings[i].Amplitude));
				}

				float biomeHeight = 0;
				foreach (float height in biomeHeights) { biomeHeight += height; }

				biomeTexture.SetPixel(x, y, DarkenColourByBiomeHeight(biomeHeight, Color.white));
			}
		}

		biomeTexture.Apply();

		return biomeTexture;
	}

	private float PerlinSample(int x, int y, float f, float a)
	{
		float xCoord = (float) x / SettingsInjecter.MapSettings.MapSize * SettingsInjecter.MapSettings.Scale + SettingsInjecter.MapSettings.OffsetX;
		float yCoord = (float) y / SettingsInjecter.MapSettings.MapSize * SettingsInjecter.MapSettings.Scale + SettingsInjecter.MapSettings.OffsetY;

		return Mathf.PerlinNoise((xCoord + SettingsInjecter.MapSettings.MapSize * SettingsInjecter.MapSettings.Seed) * f, (yCoord + SettingsInjecter.MapSettings.MapSize * SettingsInjecter.MapSettings.Seed) * f) * a;
	}
	
	private Color DarkenColourByBiomeHeight(float height, Color colour)
	{
		Color baseColour = colour;

		for (int i = 0; i < biomeHeightSteps.Length; i++)
		{
			if (height >= biomeHeightSteps[i])
				colour = Colors.Darken(baseColour, BiomeColourDiff * i);
		}
		
		return colour;
	}

	private Color GetTerrainColour(float terrainHeight, float biomeHeight, bool blend = false)
	{
		if (terrainHeight <= SettingsInjecter.MapSettings.WaterMaxHeight)
		{
			return ColourPalette.Water;
		}
		else if (terrainHeight > SettingsInjecter.MapSettings.WaterMaxHeight && terrainHeight <= SettingsInjecter.MapSettings.SandMaxHeight )
		{
			return ColourPalette.Sand;
		}
		else if (terrainHeight > SettingsInjecter.MapSettings.SandMaxHeight && terrainHeight <=  SettingsInjecter.MapSettings.DirtMaxHeight)
		{
			Color colour = ColourPalette.Dirt;

			if (blend)
				foreach (float step in biomeHeightSteps) { colour = DarkenColourByBiomeHeight(biomeHeight, colour); }

			return colour;
		}
		else if (terrainHeight > SettingsInjecter.MapSettings.DirtMaxHeight && terrainHeight <= SettingsInjecter.MapSettings.GrassMaxHeight)
		{
			Color colour = ColourPalette.Grass;

			if (blend)
				foreach (float step in biomeHeightSteps) { colour = DarkenColourByBiomeHeight(biomeHeight, colour); }

			return colour;
		}
		else if (terrainHeight > SettingsInjecter.MapSettings.GrassMaxHeight)
		{
			return ColourPalette.Stone;
		}

		return Color.black;
	}

	private void SetBiomeHeightSteps(float lower, float upper, int steps)
	{
		biomeHeightSteps = new float[steps];

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
	}
}
