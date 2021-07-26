using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Events/GameEvent ResourceGather")]
public class GameEventResourceGather : GameEvent
{
	HashSet<GameEventResourceGatherListener> resourceGatherListeners = new HashSet<GameEventResourceGatherListener>();

	public void Raise(int ID, int count)
	{
		foreach (GameEventResourceGatherListener listener in resourceGatherListeners)
		{
			listener.OnGatherEventRaised(ID, count);
		}
	}

	public void RegisterListener(GameEventResourceGatherListener listener) => resourceGatherListeners.Add(listener);
	public void UnregisterListener(GameEventResourceGatherListener listener) => resourceGatherListeners.Remove(listener);
}
