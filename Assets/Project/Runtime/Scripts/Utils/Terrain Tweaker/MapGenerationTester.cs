using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerationTester : MonoBehaviour
{
	[Header("References")]
	public SettingsInjecter SettingsInjecter;
	public ColourPalette ColourPalette;
	public SpriteRenderer SR;
	public TerrainGenerator TerrainGenerator;

	[Header("Settings")]
	public bool DisplayBiomesOnly;

	private void Update()
	{
		if (DisplayBiomesOnly)
		{
			float[,] biomeMap = TerrainGenerator.GenerateHeightMap(SettingsInjecter.MapSettings.BiomeNoiseSettings);
			SR.material.mainTexture = TerrainGenerator.GenerateTexture(biomeMap, ColourPalette, greyscale: true);
		}
		else
		{
			float[,] terrainMap = TerrainGenerator.GenerateHeightMap(SettingsInjecter.MapSettings.TerrainNoiseSettings);
			float[,] biomeMap = TerrainGenerator.GenerateHeightMap(SettingsInjecter.MapSettings.BiomeNoiseSettings);
			SR.material.mainTexture = TerrainGenerator.GenerateTextureBlend(terrainMap, biomeMap, ColourPalette);
		}
	}

}
