using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Table", menuName = "Data Packs/Item/Item Table")]
public class ItemTable : ScriptableObject
{
	public List<Item> Items = new List<Item>();

	private static ItemTable _instance;

	public static ItemTable Instance
	{
		get
		{
			if (!_instance)
				_instance = FindObjectOfType<ItemTable>();
			if (!_instance)
				_instance = CreateInstance<ItemTable>();
			return _instance;
		}
	}

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

	public string Lookup(int ID)
	{
		foreach (Item item in Items)
		{
			if (item.ID == ID) { return item.name; } 
		}
		return "";
	}

	public int Lookup(string ID)
	{
		foreach (Item item in Items)
		{
			if (item.name == name) { return item.ID; } 
		}
		return 0;
	}
}
