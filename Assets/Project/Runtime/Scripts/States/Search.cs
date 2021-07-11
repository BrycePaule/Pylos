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
		if (npcMovement.SearchItemID == 0) { npcMovement.MovementState = new Meander(npcMovement); }
		if (!TargetNeedsUpdating) { return; }

		Vector2Int found = GridHelpers.SpiralSearch(npcMovement.SearchItemID, npcMovement.TileLoc, npcMovement.SearchRange, npcMovement.SettingsInjecter.MapSettings.Tiles);
		if (found != new Vector2Int(-1, -1))
		{
			npcMovement.TargetLoc = found;
			TargetNeedsUpdating = false;
		}
		else
		{
			if (TargetNeedsUpdating)
			{
				base.FindTarget();
			}
		}
	}

	public override bool ActionAtTarget()
	{
		int x = npcMovement.TargetLoc.x;
		int y = npcMovement.TargetLoc.y;

		Container container;
		foreach (GameObject obj in npcMovement.SettingsInjecter.MapSettings.Tiles[x, y].ContainedObjects)
		{
			container = obj.GetComponent<Container>();
			if (container != null)
			{
				if (container.Contains(npcMovement.SearchItemID))
				{
					int taken = container.TakeAll(npcMovement.SearchItemID);
					Debug.Log(taken);
					npcMovement.PlayerMaterials.Increment(npcMovement.SearchItemID, taken);
					return true;
				}
			}
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

}
