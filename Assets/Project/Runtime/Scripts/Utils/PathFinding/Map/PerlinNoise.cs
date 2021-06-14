using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
	public SettingsInjecter SettingsInjecter;

    public float heightCutoff = 0.5f;

    private void FixedUpdate()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    private Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(SettingsInjecter.MapSettings.MapSize, SettingsInjecter.MapSettings.MapSize);

        // generate noise
        for (int x = 0; x < SettingsInjecter.MapSettings.MapSize; x++)
        {
            for (int y = 0; y < SettingsInjecter.MapSettings.MapSize; y++)
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
        float xCoord = (float) x / SettingsInjecter.MapSettings.MapSize * SettingsInjecter.MapSettings.Scale + SettingsInjecter.MapSettings.OffsetX;
        float yCoord = (float) y / SettingsInjecter.MapSettings.MapSize * SettingsInjecter.MapSettings.Scale + SettingsInjecter.MapSettings.OffsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        if (sample < heightCutoff) { sample = 0; }
        if (sample > heightCutoff) { sample = 1; }
        return new Color(sample, sample, sample);
    }
}
