using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public Vector2Int Loc;
	public List<TileTravelType> TravelTypes;
	public Node Parent;

	public int GCost;
	public int HCost;
	public int FCost { get { return GCost + HCost; } }

	public Node(Vector2Int _loc, List<TileTravelType> _travelTypes)
	{
		Loc = _loc;
		TravelTypes = _travelTypes;
	}
	
}
