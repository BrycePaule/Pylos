using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhaustableContainer : Container, IExhaustableContainer
{
	public ExhaustableContainer(Dictionary<ItemID, int> _items) : base(_items)
	{

	}

	public bool IsEmpty()
	{
		return items.Count == 0;
	}

	public void Exhaust()
	{
		Destroy(gameObject);
	}
	
	public override int Take(ItemID id, int count = 1)
	{
		base.Take(id, count);
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
				if (IsEmpty()) { Exhaust(); }
				return amount;
			} 
			else 
			{
				items.Remove(id);
				if (IsEmpty()) { Exhaust(); }
				return count;
			}
		}
		else
		{
			if (IsEmpty()) { Exhaust(); }
			return 0;
		}
	}
}
