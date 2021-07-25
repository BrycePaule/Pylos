using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/GameEventInt")]
public class GameEventInt : GameEvent
{
	protected new readonly List<GameEventIntListener> listeners = new List<GameEventIntListener>();

	public void Raise(int x)
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
		{
			listeners[i].OnEventRaised(x);
		}
	}
}
