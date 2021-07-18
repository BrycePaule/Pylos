using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(menuName = "Data Packs/Player Materials")]
public class PlayerMaterials : ScriptableObject
{
	public List<ItemCount> Materials;

	public int GetValue(int ID)
	{
		foreach (ItemCount itemCount in Materials)
		{
			if (itemCount.ID == ID)
			{
				return itemCount.Count;
			}
		}
		return 0;
	}

	public void Increment(int ID, int value)
	{
		foreach (ItemCount itemCount in Materials)
		{
			if (itemCount.ID == ID)
			{
				itemCount.Count += value;
				return;
			}
		}

		Materials.Add(new ItemCount(ID, value));
	}

	public void Decrement(int ID, int value)
	{
		foreach (ItemCount itemCount in Materials)
		{
			if (itemCount.ID == ID)
			{
				if (value > itemCount.Count)
				{
					itemCount.Count = 0;
				}
				else
				{
					itemCount.Count -= value;
				}
				return;
			}
		}
	}
}
