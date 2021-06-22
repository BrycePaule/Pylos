using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
	public List<ItemCount> ItemCosts;

	public Recipe(List<ItemCount> _itemCosts)
	{
		ItemCosts = _itemCosts;
	}
}
