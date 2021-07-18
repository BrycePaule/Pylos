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
		bool arrived =  GridHelpers.IsWithinDistance(npcMovement.TileLoc, npcMovement.TargetLoc, 1);

		if (arrived)
		{
			TargetNeedsUpdating = true;
		}

		return arrived;
	}

	public override void UpdatePathColour()
	{
		npcMovement.npcPathDrawer.UpdateColour(Color.red);
	}
}
