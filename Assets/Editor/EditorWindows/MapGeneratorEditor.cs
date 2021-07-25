using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

// [CustomEditor(typeof(MapGenerator))]
// public class MapGeneratorEditor : Editor 
// {
// 	public override void OnInspectorGUI() 
// 	{
// 		DrawDefaultInspector();

// 		MapGenerator mapGenerator = (MapGenerator) target;

// 		if (GUILayout.Button("Re-Generate"))
// 		{
// 			mapGenerator.CreateMap();
// 		}

// 		if (GUILayout.Button("Save Texture"))
// 		{
// 			byte[] pngData = mapGenerator.NoiseTexture.EncodeToPNG();
// 			File.WriteAllBytes(Application.dataPath + "/../Assets/Project/TextureExports/" + mapGenerator.SettingsInjecter.MapSettings.Seed + ".png", pngData);
// 		}
		
// 	}
// };
