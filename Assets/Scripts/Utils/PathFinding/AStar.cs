using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
	[SerializeField] private MapManager mapManager;
	[SerializeField] private MapGenerator mapGenerator;

	public Vector2Int targetLoc;

	private Vector2Int currLoc;
	private Node[,] nodes;

	private void Start() 
	{
		mapGenerator = transform.GetComponent<MobMovement>().MapGenerator;	
		mapManager = transform.GetComponent<MobMovement>().MapManager;	
	}

	private List<Node> FindPath()
	{
		BuildNodeGrid();
		Node startNode = nodes[currLoc.x, currLoc.y];
		Node targetNode = nodes[targetLoc.x, targetLoc.y];

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

		for (int x = 1; x <= 1; x++)
		{
			for (int y = 1; y <= 1; y++)
			{
				if (x == 0 && y == 0) { continue; }

				int checkX = node.Loc.x + x;
				int checkY = node.Loc.y + y;

				if (checkX >= 0 && checkX <= mapGenerator.MapSize && checkY >= 0 && checkY <= mapGenerator.MapSize)
				{
					neighbours.Add(nodes[checkX, checkY]);
				}
			}
		}
		
		return neighbours;
	}

	private void BuildNodeGrid()
	{
		nodes = new Node[mapGenerator.MapSize, mapGenerator.MapSize];

		for (int x = 0; x < mapGenerator.MapSize; x++)
		{
			for (int y = 0; y < mapGenerator.MapSize; y++)
			{
				Vector2Int loc = new Vector2Int(x, y);
				nodes[x, y] = new Node(loc, mapManager.IsWalkable(loc));
			}
		}
	}

	private int DistanceScore(Node nodeA, Node nodeB)
	{
		int distX = Mathf.Abs(nodeA.Loc.x - nodeB.Loc.x);
		int distY = Mathf.Abs(nodeA.Loc.y - nodeB.Loc.y);

		return 14 * Mathf.Min(distX, distY) + 10 * (Mathf.Max(distX, distX) - Mathf.Min(distX, distY));
	}
}
