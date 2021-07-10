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
	public MovementState MovementState;
	public List<TileTravelType> TravelTypes;
	public float MoveDelay;
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
		// update timer
		moveTimer += Time.deltaTime;
		searchTimer += Time.deltaTime;

		if (moveTimer >= MoveDelay)
		{
			// update target
			MovementState.FindTarget();

			// find path to target
			Path = MovementState.FindPathToTarget(maxAttempts: 3, acceptNearest: false);

			// move on path
			if (Path.Count > 0)
			{
				MovementState.Move();
				npcPathDrawer.UpdatePath(Path);
				moveTimer = 0;
			}
		}

		// if arrived, do something at the target
		if (MovementState.Arrived())
		{
			bool actioned = MovementState.ActionAtTarget();
		}
	}

	// UTILS

	public void RandomiseTimers()
	{
		moveTimer += Random.Range(0, MoveDelay);
		searchTimer += Random.Range(0, SearchDelay);
	}

}
