using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;

public class Movement : NPCComponentBase
{
	[Header("References")]
	public SettingsInjecter SettingsInjecter;

	[Header("Settings")]
	public Vector2Int TileLoc;
	public Vector2Int TargetLoc;
	private MovementState movementState;

	[Header("Searching")]
	public int SearchItemID;
	public GameEventResourceGather OnResourceGatherEvent;

	public List<Node> Path;

	[Header("NPC Components")]
	public Aggro npcAggro;
	public Combat npcCombat;
	public PathDrawer npcPathDrawer;

	private float moveTimer;
	private float searchTimer;
	private bool movementLocked;
	private bool actioned;

	protected override void Awake() 
	{
		base.Awake();
		npcBase.SubscribeComponent(NPCComponentType.Movement, this);

		movementLocked = false;
		actioned = false;
	}

	private void Start() 
	{
		npcAggro = (Aggro) npcBase.GetNPCComponent(NPCComponentType.Aggro);
		npcCombat = (Combat) npcBase.GetNPCComponent(NPCComponentType.Combat);
		npcPathDrawer = (PathDrawer) npcBase.GetNPCComponent(NPCComponentType.PathDrawer);

		RandomiseTimers();

		if (movementState == null)
			SetMovementState(new Meander(this));
	}

	private void FixedUpdate() 
	{
		Move();
	}

	private void Move()
	{
		actioned = false;

		// update timer
		moveTimer += Time.deltaTime;
		searchTimer += Time.deltaTime;

		if (!movementLocked && moveTimer >= npcBase.NPCStatAsset.MoveDelay)
		{
			// update target
			movementState.FindTarget();

			// find path to target
			Path = movementState.FindPathToTarget(maxAttempts: 3);

			// move on path
			if (Path.Count > 0)
			{
				if (!movementState.Arrived())
				{
					movementState.Move();
					moveTimer = 0;
				}
				npcPathDrawer.UpdatePath(Path);
			}
			else
			{
				moveTimer = 0;
			}
		}
		else
		{
			movementLocked = false;
		}

		// if arrived, do something at the target
		if (movementState.Arrived())
		{
			actioned = movementState.ActionAtTarget();
			if (actioned)
				movementLocked = true;
		}
	}

	// UTILS

	public void RandomiseTimers()
	{
		moveTimer += Random.Range(0, npcBase.NPCStatAsset.MoveDelay);
		searchTimer += Random.Range(0, npcBase.NPCStatAsset.SearchDelay);
	}

	public void SetMovementState(MovementState state)
	{
		movementState = state;
		movementState.UpdatePathColour();
	}

	public void ResetSearchTimer()
	{
		searchTimer = 0f;
	}

}
