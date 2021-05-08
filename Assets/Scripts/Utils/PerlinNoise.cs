using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
	[SerializeField] private MapGenerator mapGenerator;

    [Range(0, 1f)]
    public float heightCutoff = 0.5f;

    private void FixedUpdate()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    private Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(mapGenerator.MapSize, mapGenerator.MapSize);

        // generate noise
        for (int x = 0; x < mapGenerator.MapSize; x++)
        {
            for (int y = 0; y < mapGenerator.MapSize; y++)
            {
                Color colour = CalculateColour(x, y);
                texture.SetPixel(x, y, colour);    
            }   
        }

        texture.Apply();
        return texture;
    }

    private Color CalculateColour(int x, int y)
    {
        float xCoord = (float) x / mapGenerator.MapSize * mapGenerator.Scale + mapGenerator.OffsetX;
        float yCoord = (float) y / mapGenerator.MapSize * mapGenerator.Scale + mapGenerator.OffsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        if (sample < heightCutoff) { sample = 0; }
        if (sample > heightCutoff) { sample = 1; }
        return new Color(sample, sample, sample);
    }
}
