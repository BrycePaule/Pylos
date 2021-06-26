using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MovementState
{

	public Chase(Movement _npcMovement) : base(_npcMovement)
	{

	}

	public override Vector2Int FindTarget()
	{
		Vector2Int TargetLoc = npcMovement.npcAggro.AggroList.Highest.GetComponentInChildren<Movement>().TileLoc;
		return TargetLoc;
	}

	public override bool ActionAtTarget()
	{
		npcMovement.TargetLoc = npcMovement.npcAggro.AggroList.Highest.GetComponentInChildren<Movement>().TileLoc;
		return npcMovement.npcCombat.Attack(npcMovement.npcAggro.AggroList.Highest);
	}

	public override bool Arrived()
	{
		return GridHelpers.IsWithinDistance(npcMovement.TileLoc, npcMovement.TargetLoc, 1);
	}
}
