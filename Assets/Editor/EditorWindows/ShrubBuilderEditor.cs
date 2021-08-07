using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShrubBuilder))]
public class ShrubBuilderEditor : Editor
{

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		ShrubBuilder sb = (ShrubBuilder) target;

		if (GUILayout.Button("Add Objects"))
			sb.AddObject();

		if (GUILayout.Button("Reset Texture"))
			sb.ResetTexture();
	}
}
