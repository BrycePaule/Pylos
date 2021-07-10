using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class MovementState
{
	protected Movement npcMovement; 

	public bool TargetNeedsUpdating;

	public MovementState(Movement _npcMovement)
	{
		npcMovement = _npcMovement;
		TargetNeedsUpdating = true;
	}

	public virtual void Move()
	{
		Vector2Int tileLoc = npcMovement.Path[0].GlobalLoc;
		npcMovement.npcRB.MovePosition(TileConversion.TileToWorld2D(tileLoc));
		npcMovement.TileLoc = tileLoc;
	}

	public virtual void FindTarget()
	{
		if (!TargetNeedsUpdating) { return; }

		while (true)
		{
			// clamping means that on edges the units will just keep trying to path out of the map, meaning they stay on the edge
			Vector2Int potentialTarget = new Vector2Int(
				Mathf.Clamp(npcMovement.TileLoc.x + (int)Random.Range(-npcMovement.MeanderRange, npcMovement.MeanderRange), 0, npcMovement.SettingsInjecter.MapSettings.MapSize - 1),
				Mathf.Clamp(npcMovement.TileLoc.y + (int)Random.Range(-npcMovement.MeanderRange, npcMovement.MeanderRange), 0, npcMovement.SettingsInjecter.MapSettings.MapSize - 1));

			if (potentialTarget == npcMovement.TileLoc) { continue; }
			if (!npcMovement.SettingsInjecter.MapSettings.IsPathable(potentialTarget, npcMovement.TravelTypes)) { continue; }

			npcMovement.TargetLoc = potentialTarget;
			TargetNeedsUpdating = false;
			return;
		}
	}

	public virtual List<Node> FindPathToTarget(int maxAttempts, bool acceptNearest = false)
	{
		int attempts = 0;
		int searchDistance = (int) Mathf.Max(Mathf.Abs(npcMovement.TargetLoc.x - npcMovement.TileLoc.x), Mathf.Abs(npcMovement.TargetLoc.y - npcMovement.TileLoc.y));

		List<Node> path = new List<Node>();
		while (path.Count == 0)
		{
			attempts++;
			path = AStar.FindPath(npcMovement.SettingsInjecter, npcMovement.TileLoc, npcMovement.TargetLoc, searchDistance * attempts, npcMovement.TravelTypes, acceptNearest);

			if (attempts >= maxAttempts) { return new List<Node>(); }
		}

		return path;
	}

	public virtual bool Arrived()
	{
		bool arrived = GridHelpers.IsAtLocation(npcMovement.TileLoc, npcMovement.TargetLoc);

		if (arrived)
		{
			TargetNeedsUpdating = true;
		}

		return arrived;
	}

	public virtual bool ActionAtTarget()
	{
		return false;
	}
	
}
