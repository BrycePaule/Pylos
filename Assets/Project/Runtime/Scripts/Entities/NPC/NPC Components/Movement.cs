using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;

public class Movement : NPCComponentBase
{
	public MapSettings MapSettings;
	public GameSettings GameSettings;
	public PlayerMaterials PlayerMaterials;

	[Header("Movement")]
	public MovementType NPCMovementType;
	public Vector2Int TileLoc;
	public Vector2Int TargetLoc;
	public float MoveDelay;
	public float TilesPerStep; 
	public int MeanderRange;

	[Header("Searching")]
	public ItemID searchingFor;
	public GameObject FoundObject;
	public float SearchDelay;
	public int SearchRange;

	private Rigidbody2D npcRB;
	private LineRenderer lineRenderer;
	private float moveTimer;
	private float searchTimer;

	private AStar aStar;
	private Aggro npcAggro;
	private Combat npcCombat;
	private PathDrawer npcPathDrawer;

	protected override void Awake() 
	{
		base.Awake();
		npcBase.SubscribeComponent(NPCComponentType.Movement, this);

		npcRB = GetComponentInParent<Rigidbody2D>();
		aStar = GetComponent<AStar>();

		if (!CheckCanMove()) { print(npcBase.name + " doesn't have a movement type"); }
	}

	private void Start() 
	{
		npcAggro = (Aggro) npcBase.GetNPCComponent(NPCComponentType.Aggro);
		npcCombat = (Combat) npcBase.GetNPCComponent(NPCComponentType.Combat);
		npcPathDrawer = (PathDrawer) npcBase.GetNPCComponent(NPCComponentType.PathDrawer);

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
		Move();
		searchTimer += Time.deltaTime;
	}

	private void Move()
	{
		moveTimer += Time.deltaTime;

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
		int searchDistance = (int) Mathf.Max(Mathf.Abs(TargetLoc.x - TileLoc.x), Mathf.Abs(TargetLoc.y - TileLoc.y));

		List<Node> path = new List<Node>();
		while (path.Count == 0)
		{
			attempts++;
			path = aStar.FindPath(TileLoc, TargetLoc, searchDistance * attempts, npcBase.TravelTypes, acceptNearest);

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
		npcRB.MovePosition(TileConversion.TileToWorld2D(path[0].GlobalLoc));
		moveTimer = 0;

		npcPathDrawer.UpdatePath(path);
	}

	// MOVEMENT TYPES
	private void MoveMeander()
	{
		if (GridHelpers.IsAtLocation(TileLoc, TargetLoc)) { RandomTargetLocation(TileLoc); }

		if (moveTimer >= MoveDelay)
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

		if (GridHelpers.IsWithinDistance(TileLoc, TargetLoc, 1))
		{
			if (FoundObject != null)
			{
				int taken = FoundObject.GetComponent<Container>().Take(searchingFor, 1);

				FieldInfo _itemField = typeof(PlayerMaterials).GetField(searchingFor.ToString());
				_itemField.SetValue(PlayerMaterials, (int) _itemField.GetValue(PlayerMaterials) + taken);

				if (FoundObject == null) 
				{
					FoundObject = null;
					MapSettings.GetTile(TargetLoc).ContainedObjects.Remove(FoundObject);
					searchTimer += SearchDelay;
				}
			}
			else
			{
				RandomTargetLocation(TileLoc);
			}
		}

		if (moveTimer >= MoveDelay)
		{
			List<Node> path = FindPathToTarget(maxAttempts: 5, acceptNearest: true);
			TakeStepAlongPath(path);
		}
	}

	private void MoveChase()
	{
		if (npcAggro.AggroList.Highest == null) 
		{ 
			npcAggro.StopAggro(); 
			RandomTargetLocation(TileLoc);
			return;
		}

		TargetLoc = npcAggro.AggroList.Highest.GetComponentInChildren<Movement>().TileLoc;

		npcCombat.Attack(npcAggro.AggroList.Highest);
		
		if (moveTimer >= MoveDelay)
		{
			List<Node> path = FindPathToTarget(maxAttempts: 5);
			TakeStepAlongPath(path);
		}
	}

	// LOCATION GETTERS

	private GameObject FindSearchObject()
	{
		ObjectLocationPair objLocPair = GridHelpers.SpiralSearch(searchingFor, TileLoc, SearchRange, MapSettings.Tiles);
		
		if (objLocPair.obj != null) {
			TargetLoc = objLocPair.loc;
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
				Mathf.Clamp(TileLoc.x + (int) Random.Range(-MeanderRange, MeanderRange), 0, MapSettings.MapSize - 1),
				Mathf.Clamp(TileLoc.y + (int) Random.Range(-MeanderRange, MeanderRange), 0, MapSettings.MapSize - 1));

			if (potentialTarget == currentLocation) { continue; }

			if (MapSettings.IsPathable(potentialTarget, npcBase.TravelTypes))
			{
				TargetLoc = potentialTarget;
				break;
			}
		}
		return;
	}

	// UTILS

	public void RandomiseTimers()
	{
		moveTimer += Random.Range(0, MoveDelay);
		searchTimer += Random.Range(0, SearchDelay);
	}

	private bool CheckCanMove()
	{
		return npcBase.TravelTypes.Count > 0;
	}
}