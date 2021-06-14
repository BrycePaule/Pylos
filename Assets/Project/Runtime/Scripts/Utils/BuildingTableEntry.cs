using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingTableEntry
{
	public int ID;
	public string Name;
	public TileTravelType TravelType;

	public Sprite Sprite;
	public Sprite Icon;

	public GameObject Prefab;
}
