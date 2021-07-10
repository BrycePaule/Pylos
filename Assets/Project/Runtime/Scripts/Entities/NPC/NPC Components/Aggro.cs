using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggro : NPCComponentBase
{
	[Header("Settings")]
	public bool IsAggro;
	public AggroList AggroList = new AggroList();

	private Movement npcMovement;

	protected override void Awake() 
	{
		base.Awake();
		npcBase.SubscribeComponent(NPCComponentType.Aggro, this);
	}
	
	private void Start()
	{
		npcMovement = (Movement) npcBase.GetNPCComponent(NPCComponentType.Movement);
	}

	private void FixedUpdate()
	{
		if (AggroList.Count > 0)
		{
			StartAggro();
		}
		else
		{
			StopAggro();
		}
	}

	public void EnterRange(NPCBase npc)
	{
		AggroList.Increment(npc);
	}
	
	public void ExitRange(NPCBase npc)
	{
		AggroList.Remove(npc);
	}

	public void Increment(NPCBase npc, int value = 1)
	{
		AggroList.Increment(npc, value);
	}

	public void Decrement(NPCBase npc, int value = 1)
	{
		AggroList.Decrement(npc, value);
	}

	public void StartAggro()
	{
		IsAggro = true;
		npcMovement = (Movement) npcBase.GetNPCComponent(NPCComponentType.Movement);
		npcMovement.TargetLoc = ((Movement) AggroList.Highest.GetNPCComponent(NPCComponentType.Movement)).TileLoc;
		npcMovement.MovementState = new Chase(npcMovement);
	}

	public void StopAggro()
	{
		if (IsAggro)
		{
			npcMovement.MovementState = new Meander(npcMovement);
		}
		IsAggro = false;
	}

}
