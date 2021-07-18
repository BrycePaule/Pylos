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

	[Header("Searching")]
	public int SearchItemID;

	private LineRenderer lineRenderer;
	public Rigidbody2D npcRB;
	public List<Node> Path;

	public Aggro npcAggro;
	public Combat npcCombat;
	public PathDrawer npcPathDrawer;

	private float moveTimer;
	private float searchTimer;

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

		if (moveTimer >= npcBase.NPCStatAsset.MoveDelay)
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
		moveTimer += Random.Range(0, npcBase.NPCStatAsset.MoveDelay);
		searchTimer += Random.Range(0, npcBase.NPCStatAsset.SearchDelay);
	}
}
