using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
	public MapSettings MapSettings;

    [Range(0, 1f)]
    public float heightCutoff = 0.5f;

    private void FixedUpdate()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    private Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(MapSettings.MapSize, MapSettings.MapSize);

        // generate noise
        for (int x = 0; x < MapSettings.MapSize; x++)
        {
            for (int y = 0; y < MapSettings.MapSize; y++)
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
        float xCoord = (float) x / MapSettings.MapSize * MapSettings.Scale + MapSettings.OffsetX;
        float yCoord = (float) y / MapSettings.MapSize * MapSettings.Scale + MapSettings.OffsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        if (sample < heightCutoff) { sample = 0; }
        if (sample > heightCutoff) { sample = 1; }
        return new Color(sample, sample, sample);
    }
}
