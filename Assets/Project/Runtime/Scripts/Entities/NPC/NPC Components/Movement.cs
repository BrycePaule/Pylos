using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;

public class Movement : NPCComponentBase
{
	[Header("References")]
	public SettingsInjecter SettingsInjecter;
	public PlayerMaterials PlayerMaterials;

	[Header("Settings")]
	public Vector2Int TileLoc;
	public Vector2Int TargetLoc;
	public MovementType NPCMovementType;
	public float MoveDelay;
	public float TilesPerStep; 
	public int MeanderRange;

	[Header("Searching")]
	public int searchingForItemID;
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

		if (!CheckHasMovementType()) { print(npcBase.name + " doesn't have a movement type"); }
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
		if (path.Count == 0) 
		{
			moveTimer = 0;
			return;
		}

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
			List<Node> path = FindPathToTarget(maxAttempts: 3);
			TakeStepAlongPath(path);
		}
	}

	private void MoveSearch()
	{
		searchTimer += Time.deltaTime;

		// DIRT FIX FOR REPLACING ITEMID - NEEDS TO BE MADE BETTER LATER
		if (searchingForItemID == 0) { NPCMovementType = MovementType.Meander; }

		if (searchTimer >= SearchDelay && FoundObject == null)
		{
			FoundObject = FindSearchObject();
			searchTimer = 0f;
		}

		if (GridHelpers.IsWithinDistance(TileLoc, TargetLoc, 1))
		{
			if (FoundObject != null)
			{
				int taken = FoundObject.GetComponent<Container>().Take(searchingForItemID, 1);
				PlayerMaterials.Increment(searchingForItemID, taken);

				if (FoundObject == null) 
				{
					FoundObject = null;
					SettingsInjecter.MapSettings.GetTile(TargetLoc).ContainedObjects.Remove(FoundObject);
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
			List<Node> path = FindPathToTarget(maxAttempts: 3, acceptNearest: true);
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

		bool attacked = npcCombat.Attack(npcAggro.AggroList.Highest);
		
		if (moveTimer >= MoveDelay && !attacked)
		{
			List<Node> path = FindPathToTarget(maxAttempts: 3);
			TakeStepAlongPath(path);
		}
	}

	// LOCATION GETTERS

	private GameObject FindSearchObject()
	{
		ObjectLocationPair objLocPair = GridHelpers.SpiralSearch(searchingForItemID, TileLoc, SearchRange, SettingsInjecter.MapSettings.Tiles);
		
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
				Mathf.Clamp(TileLoc.x + (int)Random.Range(-MeanderRange, MeanderRange), 0, SettingsInjecter.MapSettings.MapSize - 1),
				Mathf.Clamp(TileLoc.y + (int)Random.Range(-MeanderRange, MeanderRange), 0, SettingsInjecter.MapSettings.MapSize - 1));

			if (potentialTarget == currentLocation) { continue; }

			if (SettingsInjecter.MapSettings.IsPathable(potentialTarget, npcBase.TravelTypes))
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

	private bool CheckHasMovementType()
	{
		if (npcBase.TravelTypes.Count >= 2)
		{
			return true;
		} 
		else if (npcBase.TravelTypes.Count == 1 && npcBase.TravelTypes[0] != TileTravelType.Impassable)
		{
			return true;
		}
		else 
		{
			return false;
		}
	}
}
