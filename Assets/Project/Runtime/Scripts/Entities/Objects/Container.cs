using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour, IContainer
{
	public Vector2Int TileLoc;
	public Dictionary<ItemID, int> items;
	
	private void Awake() 
	{
		items = new Dictionary<ItemID, int>();	
	}
	
	public virtual int Take(ItemID id, int count = 1)
	{
		if (count <= 0) { return 0; }

		if (items.ContainsKey(id))
		{
			if (items[id] > count) 
			{ 
				items[id] -= count;
				return count;
			}
			else if (items[id] < count)
			{
				int amount = items[id];
				items.Remove(id);
				return amount;
			} 
			else 
			{
				items.Remove(id);
				return count;
			}
		}
		else
		{
			return 0;
		}
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

	public bool Contains(ItemID id)
	{
		return items.ContainsKey(id);
	}
}
