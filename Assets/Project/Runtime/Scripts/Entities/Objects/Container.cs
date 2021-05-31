using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour, IContainer
{
	public Dictionary<ItemID, int> items;

	public Container(Dictionary<ItemID, int> _items)
	{
		items = _items;
	}
	
	private void Awake() 
	{
		items = new Dictionary<ItemID, int>();	
	}
	
	public ItemID Take(ItemID id, int count = 1)
	{
		return id;
	}
	
	public void Put(ItemID id, int count = 1)
	{
		if (items.ContainsKey(id))
		{
			items[id] += count;
		}
		else
		{
			items.Add(id, 0);
			for (int i = 0; i < count; i++)
			{
				items[id] += count;
			}
		}
	}
}
