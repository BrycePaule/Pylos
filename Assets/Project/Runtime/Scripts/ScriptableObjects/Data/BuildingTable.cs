using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Table", menuName = "Data Packs/Building/Building Table")]
public class BuildingTable : ScriptableObject
{
	public List<Building> Buildings = new List<Building>();

	private static BuildingTable _instance;

	public static BuildingTable Instance
	{
		get
		{
			if (!_instance)
				_instance = FindObjectOfType<BuildingTable>();
			if (!_instance)
				_instance = CreateInstance<BuildingTable>();
			return _instance;
		}
	}

	public Building GetById(int id)
	{
		foreach (Building entry in Buildings)
		{
			if (entry.ID == id) { return entry; } 
		}

		return null;
	}

	public Building GetByName(string name)
	{
		foreach (Building building in Buildings)
		{
			if (building.Name == name) { return building; } 
		}

		return null;
	}
}
