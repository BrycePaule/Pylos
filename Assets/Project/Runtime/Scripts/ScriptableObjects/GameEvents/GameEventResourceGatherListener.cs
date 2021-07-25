using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventResourceGatherListener : GameEventListener
{
	public new GameEventResourceGather Event;
	public new UnityEventResourceGather Response;

	public void OnEventRaised(int ID, int count)
	{
		Response.Invoke(ID, count);
	}
}
