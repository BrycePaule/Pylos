using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blackboards/PlayerResourcesBoard")]
public class PlayerResourcesBoard : Blackboard
{
	public List<ItemCount> Resources = new List<ItemCount>();
	
	private static PlayerResourcesBoard _instance;

	public static PlayerResourcesBoard Instance
	{
		get
		{
			if (!_instance)
				_instance = FindObjectOfType<PlayerResourcesBoard>();
			if (!_instance)
				_instance = CreateInstance<PlayerResourcesBoard>();
			return _instance;
		}
	}

	public int GetValue(int ID)
	{
		foreach (ItemCount itemCount in Resources)
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
		foreach (ItemCount itemCount in Resources)
		{
			if (itemCount.ID == ID)
			{
				itemCount.Count += value;
				return;
			}
		}

		Resources.Add(new ItemCount(ID, value));
	}

	public void Decrement(int ID, int value)
	{
		foreach (ItemCount itemCount in Resources)
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
