using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhaustableContainer : Container, IExhaustableContainer
{
	[Header("References")]
	public SettingsInjecter SettingsInjecter;

	public bool IsEmpty()
	{
		return items.Count == 0;
	}
	
	public override int Take(int id, int count = 1)
	{
		int taken = base.Take(id, count);
		if (IsEmpty()) { Exhaust(); }
		return taken;
	}

	public override int TakeAll(int id)
	{
		int taken = base.TakeAll(id);
		if (IsEmpty()) { Exhaust(); }
		return taken;
	}

	public void Exhaust()
	{
		MapBoard.GetTile(TileLoc).ContainedObjects.Remove(gameObject);
		Destroy(gameObject);
	}
}
