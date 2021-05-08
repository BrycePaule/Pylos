using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int GCost;
    public int HCost;

	public Node parent;
	public Vector2Int Loc;
	public bool Walkable;

	public Node(Vector2Int _loc, bool _walkable)
	{
		Loc = _loc;
		Walkable = _walkable;
	}

	public int FCost
	{
		get {
			return GCost + HCost;
		}
	}

}
