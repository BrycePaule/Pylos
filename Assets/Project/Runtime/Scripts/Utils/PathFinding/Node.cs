using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int GCost;
    public int HCost;

	public Node Parent;
	public Vector2Int GlobalLoc;
	public Vector2Int LocalLoc;
	public bool IsTravellable;

	public Node(Vector2Int _global, Vector2Int _local, bool _travellable)
	{
		GlobalLoc = _global;
		LocalLoc = _local;
		IsTravellable = _travellable;
	}

	public int FCost
	{
		get {
			return GCost + HCost;
		}
	}

}
