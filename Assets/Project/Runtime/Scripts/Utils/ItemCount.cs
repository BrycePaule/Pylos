using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemCount 
{
	public int ID;
	public int Count;

	public ItemCount(int _id, int _count)
	{
		ID = _id;
		Count = _count;
	}
}
