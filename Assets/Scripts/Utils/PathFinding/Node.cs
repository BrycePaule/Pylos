using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int GCost;
    public int HCost;

	public Node parent;
	public Vector2Int GlobalLoc;
	public Vector2Int LocalLoc;
	public bool Walkable;

	public Node(Vector2Int _global, Vector2Int _local, bool _walkable)
	{
		GlobalLoc = _global;
		LocalLoc = _local;
		Walkable = _walkable;
	}

	public int FCost
	{
		get {
			return GCost + HCost;
		}
	}

}
