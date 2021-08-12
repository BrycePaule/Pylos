using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Table", menuName = "Data Packs/Items/Item Table")]
public class ItemTable : ScriptableObject
{
	public List<ItemAsset> Items = new List<ItemAsset>();

	private static ItemTable _instance;

	public static ItemTable Instance
	{
		get
		{
			if (!_instance)
				_instance = Resources.FindObjectsOfTypeAll<ItemTable>()[0];
			// if (!_instance)
			// 	_instance = CreateInstance<ItemTable>();
			// 	Debug.Log("making a new one");
			return _instance;
		}
	}

	public ItemAsset GetById(int id)
	{
		foreach (ItemAsset item in Items)
		{
			if (item.ID == id) { return item; } 
		}

		Debug.Log("Item does not exist");
		return null;
	}

	public ItemAsset GetByName(string name)
	{
		foreach (ItemAsset item in Items)
		{
			if (item.Name == name) { return item; } 
		}

		Debug.Log("Item does not exist");
		return null;
	}

	public string Lookup(int ID)
	{
		foreach (ItemAsset item in Items)
		{
			if (item.ID == ID) { return item.name; } 
		}
		Debug.Log("Item does not exist");
		return "";
	}

	public int Lookup(string ID)
	{
		foreach (ItemAsset item in Items)
		{
			if (item.name == name) { return item.ID; } 
		}
		Debug.Log("Item does not exist");
		return 0;
	}

	public GameObject BuildItemGameObject(int id)
	{
		if (!ItemExists(id))
		{
			Debug.Log("Item does not exist");
			return null;
		}

		ItemAsset asset = GetById(id);
		GameObject itemGameObject = Instantiate(asset.Prefab);

		itemGameObject.name = asset.Name;
		// itemGameObject.transform.SetParent() 
		itemGameObject.GetComponent<ItemBase>().ID = asset.ID;
		itemGameObject.GetComponent<SpriteRenderer>().sprite = asset.Sprite;

		return itemGameObject;
	}

	// Utils
	public bool ItemExists(int id)
	{
		foreach (ItemAsset item in Items)
		{
			if (item.ID == id)
				Debug.Log($"{item.ID} {id}");
				return true;
		}
		return false;
	}
}
