using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/GameEventResourceGather")]
public class GameEventResourceGather : GameEvent
{
	protected new readonly List<GameEventResourceGatherListener> listeners = new List<GameEventResourceGatherListener>();

	public void Raise(int ID, int count)
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
		{
			listeners[i].OnEventRaised(ID, count);
		}
	}
}
