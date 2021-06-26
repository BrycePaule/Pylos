using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class MovementState
{
	protected Movement npcMovement; 

	public MovementState(Movement _npcMovement)
	{
		npcMovement = _npcMovement;
	}

	public virtual void Move()
	{
		npcMovement.npcRB.MovePosition(TileConversion.TileToWorld2D(npcMovement.Path[0].GlobalLoc));
	}

	public virtual Vector2Int FindTarget()
	{
		if (!Arrived()) { return npcMovement.TargetLoc; }

		while (true)
		{
			// clamping means that on edges the units will just keep trying to path out of the map, meaning they stay on the edge
			Vector2Int potentialTarget = new Vector2Int(
				Mathf.Clamp(npcMovement.TileLoc.x + (int)Random.Range(-npcMovement.MeanderRange, npcMovement.MeanderRange), 0, npcMovement.SettingsInjecter.MapSettings.MapSize - 1),
				Mathf.Clamp(npcMovement.TileLoc.y + (int)Random.Range(-npcMovement.MeanderRange, npcMovement.MeanderRange), 0, npcMovement.SettingsInjecter.MapSettings.MapSize - 1));

			if (potentialTarget == npcMovement.TileLoc) { continue; }
			if (!npcMovement.SettingsInjecter.MapSettings.IsPathable(potentialTarget, npcMovement.TravelTypes)) { continue; }

			return potentialTarget;
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

			if (attempts >= maxAttempts) 
			{ 
				return new List<Node>();
			}
		}

		return path;
	}

	public virtual bool Arrived()
	{
		return GridHelpers.IsAtLocation(npcMovement.TileLoc, npcMovement.TargetLoc);
	}

	public virtual bool ActionAtTarget()
	{
		return false;
	}
	
}
