using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data Packs/Item/Item Table")]
public class ItemTable : ScriptableObject
{
	public List<Item> Items = new List<Item>();

	public Item GetById(int id)
	{
		foreach (Item item in Items)
		{
			if (item.ID == id) { return item; } 
		}

		return null;
	}

	public Item GetByName(string name)
	{
		foreach (Item item in Items)
		{
			if (item.Name == name) { return item; } 
		}

		return null;
	}
}
