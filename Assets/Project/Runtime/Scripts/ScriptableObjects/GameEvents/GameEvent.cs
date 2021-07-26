using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Events/GameEvent")]
public class GameEvent : ScriptableObject
{
	protected readonly HashSet<GameEventListener> listeners = new HashSet<GameEventListener>();

	public virtual void Raise()
	{
		foreach (GameEventListener listener in listeners)
		{
			listener.OnEventRaised();
		}
	}

	public virtual void RegisterListener(GameEventListener listener) => listeners.Add(listener);
	public virtual void UnregisterListener(GameEventListener listener) => listeners.Remove(listener);
}
