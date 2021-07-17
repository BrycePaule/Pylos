using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AStar
{
	public static List<Node> FindPath(SettingsInjecter settingsInjecter, Vector2Int startGlobal, Vector2Int targetGlobal, int searchRadius, List<TileTravelType> travelTypes, bool acceptNearest = false)
	{
		int searchGridSize = (searchRadius * 2) + 1;
		Node[,] nodes = BuildNodeGrid(startGlobal, searchRadius, travelTypes, searchGridSize, settingsInjecter);

		Vector2Int startLocal = new Vector2Int(searchRadius, searchRadius);
		Vector2Int targetLocal = new Vector2Int(targetGlobal.x - startGlobal.x + searchRadius, targetGlobal.y - startGlobal.y + searchRadius);

		Node startNode = nodes[startLocal.x, startLocal.y];
		Node targetNode = nodes[targetLocal.x, targetLocal.y];

		List<Node> openSet = new List<Node>();
		List<Node> closedSet = new List<Node>();
		openSet.Add(startNode);

		while (openSet.Count > 0) 
		{
			Node currentNode = openSet[0];
			for (int i = 1; i < openSet.Count; i++)
			{
				if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost)
				{
					currentNode = openSet[i];
				}
			}

			openSet.Remove(currentNode);
			closedSet.Add(currentNode);

			if (currentNode == targetNode)
			{
				return RetracePath(startNode, targetNode);
			}

			foreach (Node neighbour in GetNeighbours(nodes, currentNode, searchGridSize))
			{
				if (!neighbour.IsTravellable || closedSet.Contains(neighbour)) { continue; }

				int newGCost = currentNode.GCost + CalculateDistanceScore(currentNode, neighbour);
				if (newGCost < neighbour.GCost || !openSet.Contains(neighbour))
				{
					neighbour.GCost = newGCost;
					neighbour.HCost = CalculateDistanceScore(neighbour, targetNode);
					neighbour.Parent = currentNode;

					if (!openSet.Contains(neighbour)) { openSet.Add(neighbour); }
				}
			}
		}

		if (acceptNearest)
		{
			Node nearTarget = null;
			foreach (Node neighbour in GetNeighbours(nodes, targetNode, searchGridSize))
			{
				if (nearTarget == null) 
				{ 
					if (neighbour.IsTravellable) { nearTarget = neighbour; } 
				}
				else
				{
					if (neighbour.GCost < nearTarget.GCost)
					{
					if (neighbour.IsTravellable) { nearTarget = neighbour; } 
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

	private static List<Node> RetracePath(Node startNode, Node endNode)
	{
		List<Node> path = new List<Node>();
		if (startNode == endNode) { return path; }

		Node currentNode = endNode;
		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.Parent;
		}

		path.Reverse();

		return path;
	}

	private static List<Node> GetNeighbours(Node[,] nodes, Node node, int searchGridSize)
	{
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0) { continue; }

				Vector2Int checkGlobal = new Vector2Int(node.GlobalLoc.x + x, node.GlobalLoc.y + y);
				Vector2Int checkLocal = new Vector2Int(node.LocalLoc.x + x, node.LocalLoc.y + y);

				if (!InsideSquareBounds(checkGlobal, MapBoard.Instance.MapSize)) { continue; }
				if (!InsideSquareBounds(checkLocal, searchGridSize)) { continue; }

				neighbours.Add(nodes[checkLocal.x, checkLocal.y]);
			}
		}
		return neighbours;
	}

	private static Node[,] BuildNodeGrid(Vector2Int startLoc, int searchRadius, List<TileTravelType> travelTypes, int searchGridSize, SettingsInjecter SI)
	{
		Node[,] nodes = new Node[searchGridSize, searchGridSize];

		for (int localX = 0; localX < searchGridSize; localX++)
		{
			for (int localY = 0; localY < searchGridSize; localY++)
			{
				Vector2Int globalLoc = new Vector2Int(startLoc.x + localX - searchRadius, startLoc.y + localY - searchRadius);

				if (!InsideSquareBounds(globalLoc, MapBoard.Instance.MapSize)) { continue; }

				nodes[localX, localY] = new Node(globalLoc, new Vector2Int(localX, localY), MapBoard.Instance.IsPathable(globalLoc, travelTypes));
			}
		}
		
		return nodes;

	}

	private static int CalculateDistanceScore(Node nodeA, Node nodeB)
	{
		int distX = Mathf.Abs(nodeA.GlobalLoc.x - nodeB.GlobalLoc.x);
		int distY = Mathf.Abs(nodeA.GlobalLoc.y - nodeB.GlobalLoc.y);
		return (14 * Mathf.Min(distX, distY)) + (10 * (Mathf.Max(distX, distY) - Mathf.Min(distX, distY)));
	}

	public static bool InsideSquareBounds(Vector2Int loc, int boundSize)
	{
		if (loc.x < 0 || loc.x >= boundSize) { return false; }
		if (loc.y < 0 || loc.y >= boundSize) { return false; }
		return true;
	}
}
