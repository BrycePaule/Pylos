using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drop Table", menuName = "Data Packs/Drops/New Drop Table")]
public class DropTable : ScriptableObject
{
	public List<DropChance> Drops = new List<DropChance>();

	public List<int> Roll()
	{
		List<int> itemsDropped = new List<int>();

		foreach (DropChance item in Drops)
		{
			if (PylosUtils.Roll(item.Chance))
				itemsDropped.Add(item.ID);
		}

		return itemsDropped;
	}
}
