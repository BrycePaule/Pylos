using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Data Packs/Building/Building")]
public class BuildingAsset : ScriptableObject
{
	public int ID;
	public string Name;
	public TileTravelType TravelType;

	public Sprite Sprite;
	public Sprite Icon;

	public GameObject Prefab;
}
