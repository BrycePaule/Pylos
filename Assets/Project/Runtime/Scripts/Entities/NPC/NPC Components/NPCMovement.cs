using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
	[SerializeField] private GameObject targetPrefab;

	public MapManager MapManager;
	public MapGenerator MapGenerator;
	public GameObject Path;
	public Vector2Int TileLoc;

	public MovementType NPCMovementType;
	public float MoveDelay;
	public float TilesPerStep; 
	public int MeanderRange;
	public int SearchRange;
	public float SearchDelay;
	public bool ShowPath;

	public float Damage;
	public float AttackSpeed;
	public int AttackRange;
	public bool Aggro;
	public GameObject AggroOn;

	public Vector2Int _targetLoc;
	private Rigidbody2D _npcRB;
	private NPCBase _npcBase;
	private LineRenderer _lineRenderer;
	private float _moveTimer;
	private AStar _aStar;

	private float attackTimer;
	public float searchTimer;

	public GameObject FoundObject;
	public ItemID searchingFor;

	private void Awake() 
	{
		_npcRB = GetComponentInParent<Rigidbody2D>();
		_npcBase = GetComponentInParent<NPCBase>();
		_aStar = GetComponent<AStar>();
		_lineRenderer = Path.GetComponent<LineRenderer>();

		if (!CheckCanMove()) { print(_npcBase.name + " doesn't have a movement type"); }
	}

	private void Start() 
	{
		_aStar.MapSize = MapGenerator.MapSize;
		_aStar.MapManager = MapManager;
		RandomTargetLocation(TileLoc);

		switch (NPCMovementType)
		{
			case MovementType.Search:
				FindSearchObject();
				break;
			default:
				RandomTargetLocation(TileLoc);
				break;
		}
	}

	private void FixedUpdate() 
	{
		if (ShowPath)
		{
			Path.SetActive(true);
		}
		else
		{
			Path.SetActive(false);
		}

		Move();
		attackTimer += Time.deltaTime;
		searchTimer += Time.deltaTime;
	}

	private void Move()
	{
		_moveTimer += Time.deltaTime;

		switch (NPCMovementType)
		{
			case MovementType.Search:
				MoveSearch();
				break;
			case MovementType.Chase:
				MoveChase();
				break;
			default:
				MoveMeander();
				break;
		}
	}

	private List<Node> FindPathToTarget(int maxAttempts, bool acceptNearest = false)
	{
		int attempts = 0;
		int searchDistance = (int) Mathf.Max(Mathf.Abs(_targetLoc.x - TileLoc.x), Mathf.Abs(_targetLoc.y - TileLoc.y));

		List<Node> path = new List<Node>();
		while (path.Count == 0)
		{
			attempts++;
			path = _aStar.FindPath(TileLoc, _targetLoc, searchDistance * attempts, _npcBase.TravelTypes, acceptNearest);

			if (attempts >= maxAttempts) 
			{ 
				return new List<Node>();
			}
		}
		return path;
	}
	
	private void TakeStepAlongPath(List<Node> path)
	{
		if (path.Count == 0) { return; }

		TileLoc = path[0].GlobalLoc;
		_npcRB.MovePosition(TileConversion.TileToWorld2D(path[0].GlobalLoc));
		_moveTimer = 0;

		if (ShowPath) { DrawPathLine(path); }
	}

	// MOVEMENT TYPES
	private void MoveMeander()
	{
		if (GridHelpers.IsAtLocation(TileLoc, _targetLoc)) { RandomTargetLocation(TileLoc); }

		if (_moveTimer >= MoveDelay)
		{
			List<Node> path = FindPathToTarget(maxAttempts: 5);
			if (path.Count == 0) 
			{
				RandomTargetLocation(TileLoc); 
			}
			else
			{
				TakeStepAlongPath(path);
			}
		}
	}

	private void MoveSearch()
	{
		if (searchingFor == ItemID.Item) { NPCMovementType = MovementType.Meander; }

		if (searchTimer >= SearchDelay && FoundObject == null)
		{
			FoundObject = FindSearchObject();
			searchTimer = 0f;
		}

		if (GridHelpers.IsWithinDistance(TileLoc, _targetLoc, 1))
		{
			if (FoundObject != null)
			{
				int taken = FoundObject.GetComponent<Container>().Take(searchingFor, 1);

				if (FoundObject == null) 
				{
					FoundObject = null;
					MapManager.GetTile(_targetLoc).ContainedObjects.Remove(FoundObject);
					searchTimer += SearchDelay;
				}
			}
			else
			{
				RandomTargetLocation(TileLoc);
			}
		}

		if (_moveTimer >= MoveDelay)
		{
			List<Node> path = FindPathToTarget(maxAttempts: 5, acceptNearest: true);
			TakeStepAlongPath(path);
		}
	}

	private void MoveChase()
	{
		if (AggroOn == null) 
		{ 
			ResetAggro(); 
			RandomTargetLocation(TileLoc);
			return;
		}

		_targetLoc = AggroOn.GetComponentInChildren<NPCMovement>().TileLoc;

		if (GridHelpers.IsWithinDistance(TileLoc, _targetLoc, AttackRange) && attackTimer >= AttackSpeed) 
		{
			AggroOn.GetComponentInChildren<Health>().Damage(Damage, AggroOn);
			attackTimer = 0f;
			return;
		} else if (GridHelpers.IsWithinDistance(TileLoc, _targetLoc, AttackRange))
		{
			return;
		}

		if (_moveTimer >= MoveDelay)
		{
			List<Node> path = FindPathToTarget(maxAttempts: 5);
			TakeStepAlongPath(path);
		}
	}

	// LOCATION GETTERS

	private GameObject FindSearchObject()
	{
		ObjectLocationPair objLocPair = GridHelpers.SpiralSearch(searchingFor, TileLoc, SearchRange, MapManager.Tiles);
		
		if (objLocPair.obj != null) {
			_targetLoc = objLocPair.loc;
			return objLocPair.obj;
		}

		return null;
	}

	public void RandomTargetLocation(Vector2Int currentLocation)
	{
		while (true)
		{
			// clamping means that on edges the units will just keep trying to path out of the map, meaning they stay on the edge
			Vector2Int potentialTarget = new Vector2Int(
				Mathf.Clamp(TileLoc.x + (int) Random.Range(-MeanderRange, MeanderRange), 0, MapGenerator.MapSize - 1),
				Mathf.Clamp(TileLoc.y + (int) Random.Range(-MeanderRange, MeanderRange), 0, MapGenerator.MapSize - 1));

			if (potentialTarget == currentLocation) { continue; }

			if (MapManager.IsPathable(potentialTarget, _npcBase.TravelTypes))
			{
				_targetLoc = potentialTarget;
				break;
			}
		}
		return;
	}

	// UTILS

	public void RandomiseMovementTick()
	{
		_moveTimer += Random.Range(0, MoveDelay);
	}

	private void DrawPathLine(List<Node> path)
	{
		Vector3[] pathArray = new Vector3[path.Count];
		for (int i = 0; i < path.Count; i++)
		{
			pathArray[i] = TileConversion.TileToWorld3D(path[i].GlobalLoc);
		}

		_lineRenderer.positionCount = path.Count;
		_lineRenderer.SetPositions(pathArray);
	}

	private bool CheckCanMove()
	{
		return _npcBase.TravelTypes.Count > 0;
	}

	public void AggroOnTo(GameObject npc)
	{
		Aggro = true;
		AggroOn = npc;
		_targetLoc = npc.GetComponentInChildren<NPCMovement>().TileLoc;
		NPCMovementType = MovementType.Chase;
	}

	private void ResetAggro()
	{
		Aggro = false;
		AggroOn = null;
		NPCMovementType = MovementType.Meander;
	}
}
