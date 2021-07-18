using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search : MovementState
{
	public Search(Movement _npcMovement) : base(_npcMovement)
	{
		_npcMovement.ResetSearchTimer();
	}

	public override void FindTarget()
	{
		if (npcMovement.SearchItemID == 0) { npcMovement.SetMovementState(new Meander(npcMovement)); }
		if (!TargetNeedsUpdating) { return; }

		Vector2Int found = GridHelpers.SpiralSearch(npcMovement.SearchItemID, npcMovement.TileLoc, npcMovement.npcBase.NPCStatAsset.SearchRange);
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

	public override List<Node> FindPathToTarget(int maxAttempts)
	{
		int attempts = 0;
		int searchDistance = (int) Mathf.Max(Mathf.Abs(npcMovement.TargetLoc.x - npcMovement.TileLoc.x), Mathf.Abs(npcMovement.TargetLoc.y - npcMovement.TileLoc.y));

		List<Node> path = new List<Node>();
		while (path.Count == 0)
		{
			attempts++;
			path = Pathfinder.Instance.FindPath(npcMovement.TileLoc, npcMovement.TargetLoc, searchDistance * attempts, npcMovement.npcBase.NPCStatAsset.TravelTypes, acceptNearest: true);

			if (attempts >= maxAttempts) { return new List<Node>(); }
		}

		return path;
	}

	public override bool ActionAtTarget()
	{
		int x = npcMovement.TargetLoc.x;
		int y = npcMovement.TargetLoc.y;

		Container container;
		foreach (GameObject obj in MapBoard.Instance.Tiles[x, y].ContainedObjects)
		{
			container = obj.GetComponent<Container>();
			if (container != null)
			{
				if (container.Contains(npcMovement.SearchItemID))
				{
					int taken = container.TakeAll(npcMovement.SearchItemID);
					PlayerResourcesBoard.Instance.Increment(npcMovement.SearchItemID, taken);
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

	public override void UpdatePathColour()
	{
		npcMovement.npcPathDrawer.UpdateColour(Color.yellow);
	}

}
