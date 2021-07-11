using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour, IContainer
{
	public Vector2Int TileLoc;
	public Dictionary<int, int> items;
	
	private void Awake() 
	{
		items = new Dictionary<int, int>();	
	}
	
	public virtual int Take(int id, int count = 1)
	{
		if (count <= 0) { return 0; }

		if (items.ContainsKey(id))
		{
			if (items[id] > count) 
			{ 
				items[id] -= count;
				return count;
			}
			else
			{
				int amount = items[id];
				items.Remove(id);
				return amount;
			} 
		}
		else
		{
			return 0;
		}
	}
	
	public virtual int TakeAll(int id)
	{
		if (items.ContainsKey(id))
		{
			int amount = items[id];
			items.Remove(id);
			return amount;
		}
		else
		{
			return 0;
		}
	}

	public void Put(int id, int count = 1)
	{
		if (items.ContainsKey(id))
		{
			items[id] += count;
		}
		else
		{
			items.Add(id, 0);
			items[id] += count;
		}
	}

	public bool Contains(int id)
	{
		return items.ContainsKey(id);
	}
}
