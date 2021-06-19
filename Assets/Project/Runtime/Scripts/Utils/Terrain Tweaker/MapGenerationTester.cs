using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerationTester : MonoBehaviour
{
	[Header("References")]
	public SpriteRenderer SR;
	public TerrainGenerator TerrainGenerator;

	[Header("Settings")]
	public bool DisplayBiomesOnly;

	private void Update()
	{
		if (DisplayBiomesOnly)
		{
			SR.material.mainTexture = TerrainGenerator.GenerateBiomeTexture();
		}
		else
		{
			SR.material.mainTexture = TerrainGenerator.GenerateWorldTexture();
		}
	}

}
