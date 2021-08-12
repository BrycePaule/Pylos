using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Table", menuName = "Data Packs/Building/Building Table")]
public class BuildingTable : ScriptableObject
{
	public List<BuildingAsset> Buildings = new List<BuildingAsset>();

	private static BuildingTable _instance;

	public static BuildingTable Instance
	{
		get
		{
			if (!_instance)
				_instance = Resources.FindObjectsOfTypeAll<BuildingTable>()[0];
			// if (!_instance)
			// 	_instance = CreateInstance<BuildingTable>();
			return _instance;
		}
	}

	public BuildingAsset GetById(int id)
	{
		foreach (BuildingAsset entry in Buildings)
		{
			if (entry.ID == id) { return entry; } 
		}

		return null;
	}

	public BuildingAsset GetByName(string name)
	{
		foreach (BuildingAsset building in Buildings)
		{
			if (building.Name == name) { return building; } 
		}

		return null;
	}
}
