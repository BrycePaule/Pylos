using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggro : NPCComponentBase
{
	[Header("Settings")]
	public bool IsAggro;
	public AggroList AggroList = new AggroList();

	private Movement npcMovement;
	private float AggroUpdateTimer;

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
		AggroUpdateTimer += Time.deltaTime;

		// if (AggroList.Count > 0)
		// {
		// 	if (!IsAggro)
		// 		StartAggro();
		// }
		// else
		// {
		// 	StopAggro();
		// }

		if (IsAggro)
		{
			if (AggroList.Count > 0)
			{
				if (AggroUpdateTimer >= npcBase.NPCStatAsset.AggroUpdateDelay)
				{
					UpdateTarget();
					AggroUpdateTimer = 0f;
				}
			}
			else
			{
				StopAggro();
			}
		}
		else
		{
			if (AggroList.Count > 0)
			{
				StartAggro();
			}
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
		UpdateTarget();
		npcMovement.SetMovementState(new Chase(npcMovement));
	}

	public void UpdateTarget()
	{
		npcMovement.TargetLoc = ((Movement) AggroList.Highest.GetNPCComponent(NPCComponentType.Movement)).TileLoc;
	}

	public void StopAggro()
	{
		if (IsAggro)
			npcMovement.SetMovementState(new Meander(npcMovement));

		IsAggro = false;
	}

}
