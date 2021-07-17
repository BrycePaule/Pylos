using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Map Settings")]
public class MapSettings : ScriptableObject
{
	[Header("Options")]
	public bool RandomiseSeed;
	public bool UseTextureFromFile;
	public string TextureName;

	[Header("Presets")]
	public float Seed;
	public int MapSize;

	[Header("Noise Generation")]
	public float Scale;
	public float OffsetX;
	public float OffsetY;
	[Space]
	public int BiomeSteps;
	public float BiomeColourDiff;
	public List<NoiseFreqAmp> TerrainNoiseSettings;
	public List<NoiseFreqAmp> BiomeNoiseSettings;

	[Header("Terrain Heights")]
	[Range(0, 1f)] public float WaterMaxHeight;
	[Range(0, 1f)] public float SandMaxHeight;
	[Range(0, 1f)] public float DirtMaxHeight;
	[Range(0, 1f)] public float GrassMaxHeight;

	[Header("Object Spawning")]
	[Range(0, 100)] public int TreeSpawnPercent; 
	[Range(0, 100)] public int ShrubSpawnPercent; 
	[Range(0, 100)] public int SandSpotSpawnPercent; 
	[Range(0, 100)] public int DirtSpotSpawnPercent; 
	[Range(0, 100)] public int StoneSpawnPercent; 
}
