using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data Packs/Building Table")]
public class BuildingTable : ScriptableObject
{
	public List<BuildingTableEntry> Buildings = new List<BuildingTableEntry>();

	public BuildingTableEntry GetById(int id)
	{
		foreach (BuildingTableEntry entry in Buildings)
		{
			if (entry.ID == id) { return entry; } 
		}

		return null;
	}
}
