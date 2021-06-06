using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : NPCComponentBase
{
	public float Damage;
	public float AttackSpeed;
	public int AttackRange;

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

		if (GridHelpers.IsWithinDistance(npcMovement.TileLoc, npcMovement.TargetLoc, AttackRange) && attackTimer >= AttackSpeed) 
		{
			target.GetComponentInChildren<Health>().Damage(Damage, npcBase);
			attackTimer = 0f;
			return true;
		} else
		{
			return false;
		}
	}
}
