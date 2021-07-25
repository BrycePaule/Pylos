using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventIntListener : GameEventListener
{
	public new GameEventInt Event;
	public new UnityEventInt Response;

	public void OnEventRaised(int x)
	{
		Response.Invoke(x);
	}
}
