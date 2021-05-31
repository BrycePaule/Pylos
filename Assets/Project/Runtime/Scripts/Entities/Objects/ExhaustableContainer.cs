using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhaustableContainer : Container, IExhaustableContainer
{
	public ExhaustableContainer(Dictionary<ItemID, int> _items) : base(_items)
	{

	}

	public bool CheckEmpty()
	{
		return items.Count == 0;
	}

	public void Exhaust()
	{
		Destroy(gameObject);
	}

	// private void FixedUpdate() 
	// {
	// 	Put(ItemID.Wood, 1);
	// }
}
