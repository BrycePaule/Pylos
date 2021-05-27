using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
	[SerializeField] private GameObject targetPrefab;
	[SerializeField] private GameObject searchingFor;

	public MapManager MapManager;
	public MapGenerator MapGenerator;
	public GameObject Path;
	public Vector2Int TileLoc;
	public bool ShowPath;

	public MovementType NPCMovementType;
	public float MoveDelay;
	public float TilesPerStep; 
	public int MeanderRange;
	public int SearchRange;

	public float Damage;
	public float AttackSpeed;
	public int AttackRange;
	public bool Aggro;
	public GameObject AggroOn;

	public GameObject FoundObject;

	public Vector2Int _targetLoc;
	private Rigidbody2D _npcRB;
	private NPCBase _npcBase;
	private LineRenderer _lineRenderer;
	private float _moveTimer;
	private AStar _aStar;

	private float attackTimer;

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
		_targetLoc = new Vector2Int(-1, -1);
		
		switch (NPCMovementType)
		{
			case MovementType.Search:
				FindSearchObjectLocation();
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
	}

	private void Move()
	{
		_moveTimer += Time.deltaTime;

		switch (NPCMovementType)
		{
			case MovementType.Search:
				Search();
				break;
			case MovementType.Chase:
				Chase();
				break;
			default:
				Meander();
				break;
		}
	}

	private List<Node> FindPathToTarget(int maxAttempts)
	{
		int attempts = 0;
		int searchDistance = (int) Mathf.Max(Mathf.Abs(_targetLoc.x - TileLoc.x), Mathf.Abs(_targetLoc.y - TileLoc.y));

		List<Node> path = new List<Node>();
		while (path.Count == 0)
		{
			attempts++;
			path = _aStar.FindPath(TileLoc, _targetLoc, searchDistance * attempts, _npcBase.TravelTypes);

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
	private void Meander()
	{
		if (IsAtTargetLocation()) { RandomTargetLocation(TileLoc); }

		if (_moveTimer >= MoveDelay)
		{
			List<Node> path = FindPathToTarget(maxAttempts: 5);
			if (path.Count == 0) { RandomTargetLocation(TileLoc); }
			TakeStepAlongPath(path);
		}
	}

	private void Search()
	{
		if (searchingFor == null) { NPCMovementType = MovementType.Meander; }

		if (FoundObject == null) 
		{
			print("no object found");
			FoundObject = FindSearchObjectLocation();
			if (IsAtTargetLocation()) { print("arrived bitch"); RandomTargetLocation(TileLoc); }
		}
		else
		{
			if (IsNextToTargetLocation())
			{
				print("wood +1");
				_npcBase.Wood +=1;

				MapManager.GetTile(_targetLoc).ContainedObjects.Remove(FoundObject);
				Destroy(FoundObject);

				FoundObject = null;
				_moveTimer = 0;
			}
		}

		if (_moveTimer >= MoveDelay)
		{
			List<Node> path = FindPathToTarget(maxAttempts: 5);
			TakeStepAlongPath(path);
		}
	}

	private void Chase()
	{
		if (AggroOn == null) 
		{ 
			ResetAggro(); 
			RandomTargetLocation(TileLoc);
			return;
		}

		_targetLoc = AggroOn.GetComponentInChildren<NPCMovement>().TileLoc;

		if (IsWithinDistance(AttackRange) && attackTimer >= AttackSpeed) 
		{
			AggroOn.GetComponentInChildren<Health>().Damage(1);
			attackTimer = 0f;
			return;
		}

		if (_moveTimer >= MoveDelay)
		{
			List<Node> path = FindPathToTarget(maxAttempts: 5);
			TakeStepAlongPath(path);
		}
	}

	// LOCATION GETTERS

	private GameObject FindSearchObjectLocation()
	{
		// atm this will always find the top-left-most thing first, needs to be fixed
		for (int y = TileLoc.y - SearchRange; y <= TileLoc.y + SearchRange; y++)
		{
			for (int x = TileLoc.x - SearchRange; x <= TileLoc.x + SearchRange; x++)
			{
				Vector2Int searchLoc = new Vector2Int(x, y);
				if (!MapManager.IsWithinBounds(searchLoc)) { continue; }

				foreach (GameObject obj in MapManager.GetTile(searchLoc).ContainedObjects)
				{
					if (obj.GetType() == searchingFor.GetType())
					{
						print("found a tree");
						_targetLoc = searchLoc;
						return obj;
					}
				}
			}
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

			if (_npcBase.CanWalk)
			{
				if (MapManager.GetTile(potentialTarget).IsWalkable) 
				{
					_targetLoc = potentialTarget;
					break;
				}
			}

			if (_npcBase.CanSwim)
			{
				if (MapManager.GetTile(potentialTarget).IsSwimmable) 
				{
					_targetLoc = potentialTarget;
					break;
				}
			}
		}
		// UpdateTargetMarker();
		return;
	}

	// UTILS

	public void RandomiseMovementTick()
	{
		_moveTimer += Random.Range(0, MoveDelay);
	}

	private bool IsAtTargetLocation()
	{
		return (TileLoc == _targetLoc) ? true : false;
	}
	
	private bool IsNextToTargetLocation()
	{
		if (Mathf.Abs(TileLoc.x - _targetLoc.x) > 1) { return false; }
		if (Mathf.Abs(TileLoc.y - _targetLoc.y) > 1) { return false; }
		return true;
	}

	private bool IsWithinDistance(int distance)
	{
		if (Mathf.Abs(TileLoc.x - _targetLoc.x) > distance) { return false; }
		if (Mathf.Abs(TileLoc.y - _targetLoc.y) > distance) { return false; }
		return true;
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
