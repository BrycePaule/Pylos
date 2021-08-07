using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RuntimeUtility))]
public class RuntimeUtilityEditor : Editor
{
	int timeScale = 1;

	public override void OnInspectorGUI()
	{
		RuntimeUtility runUtil = (RuntimeUtility) target;

		if (GUILayout.Button("Increment (+0.1)"))
			runUtil.IncrementTimescale();

		if (GUILayout.Button("Decrement (-0.1)"))
			runUtil.DecrementTimescale();

		EditorGUILayout.Space(10);
		
		timeScale = EditorGUILayout.IntField("Time Scale", timeScale);
		if (GUILayout.Button("Set Timescale"))
			runUtil.SetTimescale(timeScale);
	}
}


