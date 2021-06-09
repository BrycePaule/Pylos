using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Game Settings")]
public class GameSettings : ScriptableObject
{
	[Header("Debug")]
	public bool ShowPaths;
	public bool ShowDetectionRadius;
	public bool MenuIsOpen;

	[Header("Camera Settings")]
	public float CameraScrollSpeed;
}
