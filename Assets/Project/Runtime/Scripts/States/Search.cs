using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search : MovementState
{
	
	public Search(Movement _npcMovement) : base(_npcMovement)
	{

	}

	public override void FindTarget()
	{
		if (npcMovement.searchingForItemID == 0) { npcMovement.MovementState = new Meander(npcMovement); }
	}

	public override bool ActionAtTarget()
	{
		return base.ActionAtTarget();
	}

	public override bool Arrived()
	{
		return base.Arrived();
	}

	

	// private void MoveSearch()
	// {
	// 	if (searchTimer >= SearchDelay && FoundObject == null)
	// 	{
	// 		FoundObject = FindSearchObject();
	// 		searchTimer = 0f;
	// 	}

	// 	if (GridHelpers.IsWithinDistance(TileLoc, TargetLoc, 1))
	// 	{
	// 		if (FoundObject != null)
	// 		{
	// 			int taken = FoundObject.GetComponent<Container>().Take(searchingForItemID, 1);
	// 			PlayerMaterials.Increment(searchingForItemID, taken);

	// 			if (FoundObject == null) 
	// 			{
	// 				FoundObject = null;
	// 				SettingsInjecter.MapSettings.GetTile(TargetLoc).ContainedObjects.Remove(FoundObject);
	// 				searchTimer += SearchDelay;
	// 			}
	// 		}
	// 		else
	// 		{
	// 			// RandomTargetLocation(TileLoc);
	// 		}
	// 	}

	// 	if (moveTimer >= MoveDelay)
	// 	{
	// 		Path = MovementState.FindPathToTarget(this, maxAttempts: 3, acceptNearest: true);
	// 	}
	// }

	// private GameObject FindSearchObject()
	// {
	// 	ObjectLocationPair objLocPair = GridHelpers.SpiralSearch(searchingForItemID, TileLoc, SearchRange, SettingsInjecter.MapSettings.Tiles);

	// 	if (objLocPair.obj != null) {
	// 		TargetLoc = objLocPair.loc;
	// 		return objLocPair.obj;
	// 	}

	// 	return null;
	// }

}
