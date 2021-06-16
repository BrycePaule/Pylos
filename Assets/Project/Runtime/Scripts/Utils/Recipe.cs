using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
	public List<ItemCost> ItemCosts;

	public Recipe(List<ItemCost> _itemCosts)
	{
		ItemCosts = _itemCosts;
	}
}
