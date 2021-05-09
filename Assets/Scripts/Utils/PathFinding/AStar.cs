using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
	public int MapSize;
	public MapManager MapManager;

	private Node[,] nodes;
	private int searchGridSize;

	public List<Node> FindPath(Vector2Int startLocGlobal, Vector2Int targetLocGlobal, int halfSize)
	{
		searchGridSize = (halfSize * 2) + 1;
		BuildNodeGrid(startLocGlobal, halfSize);

		Vector2Int startLocLocal = new Vector2Int(halfSize, halfSize);
		Vector2Int targetLocLocal = new Vector2Int(targetLocGlobal.x - startLocGlobal.x + halfSize, targetLocGlobal.y - startLocGlobal.y + halfSize);

		Node startNode = nodes[startLocLocal.x, startLocLocal.y];
		Node targetNode = nodes[targetLocLocal.x, targetLocLocal.y];

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

			foreach (Node neighbour in GetNeighbours(currentNode))
			{
				if (!neighbour.Walkable || closedSet.Contains(neighbour)) { continue; }

				int newGCost = currentNode.GCost + DistanceScore(currentNode, neighbour);
				if (newGCost < neighbour.GCost || !openSet.Contains(neighbour))
				{
					neighbour.GCost = newGCost;
					neighbour.HCost = DistanceScore(neighbour, targetNode);
					neighbour.parent = currentNode;

					if (!openSet.Contains(neighbour)) { openSet.Add(neighbour); }
				}
			}
		}
		return new List<Node>();
	}

	private List<Node> RetracePath(Node startNode, Node endNode)
	{
		List<Node> path = new List<Node>();

		Node currentNode = endNode;
		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}

		path.Reverse();

		return path;
	}

	private List<Node> GetNeighbours(Node node)
	{
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0) { continue; }

				Vector2Int checkGlobal = new Vector2Int(node.GlobalLoc.x + x, node.GlobalLoc.y + y);
				Vector2Int checkLocal = new Vector2Int(node.LocalLoc.x + x, node.LocalLoc.y + y);

				if (!InsideGlobalBounds(checkGlobal)) { continue; }
				if (!InsideLocalBounds(checkLocal)) { continue; }

				neighbours.Add(nodes[checkLocal.x, checkLocal.y]);
			}
		}
		
		return neighbours;
	}

	private void BuildNodeGrid(Vector2Int startLoc, int halfSize)
	{
		nodes = new Node[searchGridSize, searchGridSize];
		for (int localX = 0; localX < searchGridSize; localX++)
		{
			for (int localY = 0; localY < searchGridSize; localY++)
			{
				Vector2Int globalLoc = new Vector2Int(startLoc.x + localX - halfSize, startLoc.y + localY - halfSize);

				if (!InsideGlobalBounds(globalLoc)) { continue; }

				nodes[localX, localY] = new Node(globalLoc, new Vector2Int(localX, localY), MapManager.IsWalkable(globalLoc));
			}
		}
	}

	private static int DistanceScore(Node nodeA, Node nodeB)
	{
		int distX = Mathf.Abs(nodeA.GlobalLoc.x - nodeB.GlobalLoc.x);
		int distY = Mathf.Abs(nodeA.GlobalLoc.y - nodeB.GlobalLoc.y);

		return 14 * Mathf.Min(distX, distY) + 10 * (Mathf.Max(distX, distX) - Mathf.Min(distX, distY));
	}

	public bool InsideGlobalBounds(Vector2Int loc)
	{
		if (loc.x < 0 || loc.x >= MapSize) { return false; }
		if (loc.y < 0 || loc.y >= MapSize) { return false; }
		return true;
	}

	private bool InsideLocalBounds(Vector2Int loc)
	{
		if (loc.x < 0 || loc.x >= searchGridSize) { return false; }
		if (loc.y < 0 || loc.y >= searchGridSize) { return false; }
		return true;
	}
}
