using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLocationPair
{
	public GameObject obj;
	public Vector2Int loc;

	public ObjectLocationPair(GameObject _obj, Vector2Int _loc)
	{
		obj = _obj;
		loc = _loc;
	}
}
