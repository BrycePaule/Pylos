using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameEventResourceGatherListener : GameEventListener
{
	public GameEventResourceGather GatherEvent;
	public UnityEventResourceGather GatherResponse;

	public override void OnEnable() => GatherEvent.RegisterListener(this);
	public override void OnDisable() => GatherEvent.RegisterListener(this);

	public void OnGatherEventRaised(int ID, int count)
	{
		GatherResponse.Invoke(ID, count);
	}
}
