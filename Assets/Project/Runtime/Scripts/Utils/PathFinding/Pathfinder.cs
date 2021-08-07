using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : ScriptableObject
{
	private static Pathfinder _instance;

	private Node[,] nodeGrid;

	public static Pathfinder Instance
	{
		get
		{
			if (!_instance)
				_instance = FindObjectOfType<Pathfinder>();
			if (!_instance)
				_instance = CreateInstance<Pathfinder>();
			return _instance;
		}
	}

	public void Initialise(int mapSize)
	{
		nodeGrid = new Node[mapSize, mapSize];
	}

	public List<Node> FindPath(Vector2Int start, Vector2Int target, int searchRadius, List<TileTravelType> validTravelTypes, bool acceptNearest = false)
	{
		RebuildLocalGrid(start, searchRadius, validTravelTypes);
		if (nodeGrid[target.x, target.y] == null)
			nodeGrid[target.x, target.y] = new Node(target, MapBoard.Instance.GetTile(target).TravelTypes);

		Node startNode = nodeGrid[start.x, start.y];
		Node targetNode = nodeGrid[target.x, target.y];

		List<Node> uncheckedNodes = new List<Node>();
		List<Node> checkedNodes = new List<Node>();
		uncheckedNodes.Add(startNode);

		while (uncheckedNodes.Count > 0) 
		{
			Node currentNode = uncheckedNodes[0];
			for (int i = 1; i < uncheckedNodes.Count; i++)
			{
				if (uncheckedNodes[i].FCost < currentNode.FCost || uncheckedNodes[i].FCost == currentNode.FCost && uncheckedNodes[i].HCost < currentNode.HCost)
				{
					currentNode = uncheckedNodes[i];
				}
			}

			uncheckedNodes.Remove(currentNode);
			checkedNodes.Add(currentNode);

			if (currentNode == targetNode)
			{
				return RetracePath(startNode, targetNode);
			}

			foreach (Node neighbour in GetNeighbours(currentNode, searchRadius, start))
			{
				if (!NodeContainsValidTravelType(neighbour, validTravelTypes) || checkedNodes.Contains(neighbour)) { continue; }

				int newGCost = currentNode.GCost + CalculateDistanceScore(currentNode, neighbour);
				if (newGCost < neighbour.GCost || !uncheckedNodes.Contains(neighbour))
				{
					neighbour.GCost = newGCost;
					neighbour.HCost = CalculateDistanceScore(neighbour, targetNode);
					neighbour.Parent = currentNode;

					if (!uncheckedNodes.Contains(neighbour)) { uncheckedNodes.Add(neighbour); }
				}
			}
		}

		if (acceptNearest)
		{
			Node nearTarget = null;
			foreach (Node neighbour in GetNeighbours(targetNode, searchRadius, start))
			{
				if (nearTarget == null) 
				{ 
					if (NodeContainsValidTravelType(neighbour, validTravelTypes)) { nearTarget = neighbour; } 
				}
				else
				{
					if (neighbour.GCost < nearTarget.GCost)
					{
					if (NodeContainsValidTravelType(neighbour, validTravelTypes)) { nearTarget = neighbour; } 
					}
				}
			}

			if (nearTarget != null) 
			{
				return RetracePath(startNode, nearTarget);
			}
		}

		return new List<Node>();
	}

	private void RebuildLocalGrid(Vector2Int centre, int searchRadius, List<TileTravelType> validTravelTypes)
	{
		for (int x = -searchRadius; x < searchRadius; x++)
		{
			for (int y = -searchRadius; y < searchRadius; y++)
			{
				Vector2Int loc = new Vector2Int(centre.x + x, centre.y + y);

				if (InsideSquareBounds(loc, 0, MapBoard.Instance.MapSize)) 
				{
					if (nodeGrid[loc.x, loc.y] == null)
						nodeGrid[loc.x, loc.y] = new Node(loc, MapBoard.Instance.GetTile(loc).TravelTypes);
					else
						nodeGrid[loc.x, loc.y].TravelTypes = MapBoard.Instance.GetTile(loc).TravelTypes;
				}
			}
		}
	}

	private List<Node> GetNeighbours(Node node, int searchRadius, Vector2Int startLoc)
	{
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0) { continue; }

				Vector2Int neighbour = new Vector2Int(node.Loc.x + x, node.Loc.y + y);
				if (!InsideSquareBounds(neighbour - startLoc, -searchRadius, searchRadius)) { continue; }
				if (!InsideSquareBounds(neighbour, 0, MapBoard.Instance.MapSize)) { continue; }

				neighbours.Add(nodeGrid[neighbour.x, neighbour.y]);
			}
		}

		return neighbours;
	}

	private static List<Node> RetracePath(Node startNode, Node targetNode)
	{
		List<Node> path = new List<Node>();
		if (startNode == targetNode) { return path; }

		Node currentNode = targetNode;
		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.Parent;
		}

		path.Reverse();

		return path;
	}

	private static int CalculateDistanceScore(Node nodeA, Node nodeB)
	{
		int distX = Mathf.Abs(nodeA.Loc.x - nodeB.Loc.x);
		int distY = Mathf.Abs(nodeA.Loc.y - nodeB.Loc.y);
		return (14 * Mathf.Min(distX, distY)) + (10 * (Mathf.Max(distX, distY) - Mathf.Min(distX, distY)));
	}

	public static bool InsideSquareBounds(Vector2Int loc, int lower, int upper)
	{
		if (loc.x < lower || loc.x >= upper) { return false; }
		if (loc.y < lower || loc.y >= upper) { return false; }
		return true;
	}

	private static bool NodeContainsValidTravelType(Node node, List<TileTravelType> validTravelTypes)
	{
		if (node.TravelTypes.Contains(TileTravelType.Impassable)) { return false; }

		foreach (TileTravelType travelType in node.TravelTypes)
		{
			if (validTravelTypes.Contains(travelType)) { return true; }
		}
		return false;
	}
}
