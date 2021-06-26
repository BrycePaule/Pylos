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
	public MovementState MovementState;
	public List<TileTravelType> TravelTypes;
	public float MoveDelay;
	public float PathfindDelay;
	public float TilesPerStep; 
	public int MeanderRange;

	[Header("Searching")]
	public int searchingForItemID;
	public GameObject FoundObject;
	public float SearchDelay;
	public int SearchRange;

	public Rigidbody2D npcRB;
	private LineRenderer lineRenderer;
	private float moveTimer;
	private float pathfindTimer;
	private float searchTimer;
	public List<Node> Path;

	public Aggro npcAggro;
	public Combat npcCombat;
	public PathDrawer npcPathDrawer;

	protected override void Awake() 
	{
		base.Awake();
		npcBase.SubscribeComponent(NPCComponentType.Movement, this);

		npcRB = GetComponentInParent<Rigidbody2D>();
	}

	private void Start() 
	{
		npcAggro = (Aggro) npcBase.GetNPCComponent(NPCComponentType.Aggro);
		npcCombat = (Combat) npcBase.GetNPCComponent(NPCComponentType.Combat);
		npcPathDrawer = (PathDrawer) npcBase.GetNPCComponent(NPCComponentType.PathDrawer);

		RandomiseTimers();
	}

	private void FixedUpdate() 
	{
		Move();
	}

	private void Move()
	{
		print("move");

		// update timer
		moveTimer += Time.deltaTime;
		// pathfindTimer += Time.deltaTime;
		searchTimer += Time.deltaTime;

		// update target
		TargetLoc = MovementState.FindTarget();
		if (TargetLoc != new Vector2Int(-1, -1)) 
		{
			if (moveTimer >= MoveDelay)
			{
				// find path to target
				Path = MovementState.FindPathToTarget(maxAttempts: 3, acceptNearest: false);
				// pathfindTimer = 0;

				// move on path
				MovementState.Move();
				npcPathDrawer.UpdatePath(Path);
				moveTimer = 0;
			}
		}

		// if arrived, do something at the target
		if (MovementState.Arrived())
		{
			bool actioned = MovementState.ActionAtTarget();
			pathfindTimer += PathfindDelay;
		}
	}

	// UTILS

	public void RandomiseTimers()
	{
		moveTimer += Random.Range(0, MoveDelay);
		searchTimer += Random.Range(0, SearchDelay);
		pathfindTimer += Random.Range(0, PathfindDelay);
	}

}
