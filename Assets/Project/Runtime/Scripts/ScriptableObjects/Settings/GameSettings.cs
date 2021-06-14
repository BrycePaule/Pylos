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
	public bool BuildMenuIsOpen;

	[Header("Camera Settings")]
	public float CameraScrollSpeed;
	public float CameraBoostScrollSpeed;

	[Header("Building")]
	public bool IsBuilding;
}
