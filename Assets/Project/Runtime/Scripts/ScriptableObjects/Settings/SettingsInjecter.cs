using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Settings Injecter")]
public class SettingsInjecter : ScriptableObject
{
	[Header("Settings")]
	public GameSettings GameSettings;
	public MapSettings MapSettings;
	public NPCSettings NPCSettings;
	
	[Header("Lookup Tables")]
	public ItemTable ItemTable;
	public BuildingTable BuildingTable;
}
