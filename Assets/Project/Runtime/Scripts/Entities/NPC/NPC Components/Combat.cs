using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : NPCComponentBase
{
	private float attackTimer;

	private Movement npcMovement;
	private Aggro npcAggro;

	protected override void Awake() 
	{
		base.Awake();
		npcBase.SubscribeComponent(NPCComponentType.Combat, this);
	}
	
	private void Start()
	{
		npcMovement = (Movement) npcBase.GetNPCComponent(NPCComponentType.Movement);
		npcAggro = (Aggro) npcBase.GetNPCComponent(NPCComponentType.Aggro);
	}

	private void FixedUpdate() 
	{
		attackTimer += Time.deltaTime;
	}

	public bool Attack(NPCBase target)
	{
		npcMovement.TargetLoc = npcAggro.AggroList.Highest.GetComponentInChildren<Movement>().TileLoc;

		if (GridHelpers.IsWithinDistance(npcMovement.TileLoc, npcMovement.TargetLoc, npcBase.NPCStatAsset.AttackRange) && attackTimer >= npcBase.NPCStatAsset.AttackSpeed) 
		{
			target.GetComponentInChildren<Health>().Damage(npcBase.NPCStatAsset.Damage, npcBase);
			attackTimer = 0f;
			return true;
		} else
		{
			return false;
		}
	}
}
