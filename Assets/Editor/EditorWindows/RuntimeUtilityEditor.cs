using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RuntimeUtility))]
public class RuntimeUtilityEditor : Editor
{
	public override void OnInspectorGUI()
	{
		RuntimeUtility runUtil = (RuntimeUtility) target;

		if (GUILayout.Button("Increase Timescale"))
			runUtil.IncrementTimescale();

		if (GUILayout.Button("Decrease Timescale"))
			runUtil.DecrementTimescale();
	}
}


