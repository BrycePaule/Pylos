using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Table", menuName = "Data Packs/Building/Building Table")]
public class BuildingTable : ScriptableObject
{
	public List<Building> Buildings = new List<Building>();

	public Building GetById(int id)
	{
		foreach (Building entry in Buildings)
		{
			if (entry.ID == id) { return entry; } 
		}

		return null;
	}
}
