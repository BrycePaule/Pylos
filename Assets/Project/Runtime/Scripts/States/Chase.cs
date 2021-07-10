using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MovementState
{

	public Chase(Movement _npcMovement) : base(_npcMovement)
	{

	}

	public override void FindTarget()
	{
		NPCBase target = npcMovement.npcAggro.AggroList.Highest;
		
		if (target != null)
		{
			npcMovement.TargetLoc = target.GetComponentInChildren<Movement>().TileLoc;
		}
	}

	public override bool ActionAtTarget()
	{
		NPCBase target = npcMovement.npcAggro.AggroList.Highest;

		if (target != null)
		{
			npcMovement.TargetLoc = target.GetComponentInChildren<Movement>().TileLoc;
			return npcMovement.npcCombat.Attack(npcMovement.npcAggro.AggroList.Highest);
		}

		return false;
	}

	public override bool Arrived()
	{
		return GridHelpers.IsWithinDistance(npcMovement.TileLoc, npcMovement.TargetLoc, 1);
	}
}
