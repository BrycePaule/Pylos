using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
	[SerializeField] private GameObject targetPrefab;

	public MapManager MapManager;
	public MapGenerator MapGenerator;
	public Vector2Int TileLoc;

	public float MoveDelay;
	public float TilesPerStep; 
	public int MeanderDistance;

	private Vector2Int _targetLoc;
	private Rigidbody2D _mobRB;
	private MobBase _mobBase;
	private float _moveTimer;
	private AStar _aStar;

	private GameObject targetMarker;

	private void Awake() 
	{
		_mobRB = GetComponentInParent<Rigidbody2D>();
		_mobBase = GetComponentInParent<MobBase>();
		_aStar = GetComponent<AStar>();
	}

	private void Start() 
	{
		_aStar.MapSize = MapGenerator.MapSize;
		_aStar.MapManager = MapManager;

		// targetMarker = Instantiate(targetPrefab, Vector3.zero, Quaternion.identity, MapManager.transform);
	}

	private void FixedUpdate() 
	{
		Move2();
	}

	private void Move()
	{
		_moveTimer += Time.deltaTime;

		if (TileLoc == _targetLoc || _targetLoc == Vector2.zero) { _targetLoc = SelectNewTargetLocation(); }

		if (_moveTimer >= MoveDelay)
		{
			int dx = (int) Mathf.Clamp(_targetLoc.x - TileLoc.x, -1, 1);
			int dy = (int) Mathf.Clamp(_targetLoc.y - TileLoc.y, -1, 1);

			Vector2Int dest = TileLoc + new Vector2Int(dx, dy);
			GroundTileData destTile = MapManager.GetTile(dest);

			if (_mobBase.CanWalk)
			{
				if (destTile.IsWalkable)
				{
					_mobRB.MovePosition(TileConversion.TileToWorld2D(dest));
					TileLoc = dest;
					_moveTimer = 0;
					return;
				}
			}

			if (_mobBase.CanSwim)
			{
				if (destTile.IsSwimmable)
				{
					_mobRB.MovePosition(TileConversion.TileToWorld2D(dest));
					TileLoc = dest;
					_moveTimer = 0;
					return;
				}
			}
		}
	}

	/*
		TODO:  
			check if can find a path, if not expand pathfinding search (will allow for most cases needing a small scale search, but as units
			struggle to find a path they will actually look for better ones)
	*/
	private void Move2()
	{
		_moveTimer += Time.deltaTime;

		if (TileLoc == _targetLoc || _targetLoc == Vector2.zero) { _targetLoc = SelectNewTargetLocation(); }

		int pathfindAttempts = 0;
		if (_moveTimer >= MoveDelay)
		{
			int maxDistance = (int) Mathf.Max(Mathf.Abs(_targetLoc.x - TileLoc.x), Mathf.Abs(_targetLoc.y - TileLoc.y));

			List<Node> path = new List<Node>();
			while (path.Count == 0)
			{
				pathfindAttempts++;
				path = _aStar.FindPath(TileLoc, _targetLoc, maxDistance * pathfindAttempts);

				if (pathfindAttempts >= 5) 
				{ 
					_targetLoc = SelectNewTargetLocation(); 
					return;
				}
			}

			_mobRB.MovePosition(TileConversion.TileToWorld2D(path[0].GlobalLoc));
			TileLoc = path[0].GlobalLoc;
			_moveTimer = 0;
		}

	}

	public Vector2Int SelectNewTargetLocation()
	{
		Vector2Int newTarget = Vector2Int.zero;

		while (newTarget == Vector2Int.zero)
		{
			Vector2Int potentialTarget = new Vector2Int(
				Mathf.Clamp(TileLoc.x + (int) Random.Range(-MeanderDistance, MeanderDistance), 0, MapGenerator.MapSize - 1),
				Mathf.Clamp(TileLoc.y + (int) Random.Range(-MeanderDistance, MeanderDistance), 0, MapGenerator.MapSize - 1));

			if (_mobBase.CanWalk)
			{
				if (MapManager.GetTile(potentialTarget).IsWalkable) 
				{
					newTarget = potentialTarget;
					break; 
				}
			}

			if (_mobBase.CanSwim)
			{
				if (MapManager.GetTile(potentialTarget).IsSwimmable) 
				{
					newTarget = potentialTarget;
					break;
				}
			}
		}

		// targetMarker.transform.position = TileConversion.TileToWorld3D(newTarget);
		return newTarget;
	}

	public void RandomiseMovementTick()
	{
		_moveTimer += Random.Range(0, MoveDelay);
	}
}
