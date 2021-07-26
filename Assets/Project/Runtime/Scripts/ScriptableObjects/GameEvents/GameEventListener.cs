using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameEventListener : MonoBehaviour
{
	public GameEvent Event;
	public UnityEvent Response;

	public virtual void OnEnable() => Event.RegisterListener(this);
	public virtual void OnDisable() => Event.RegisterListener(this);
	
	public virtual void OnEventRaised()
	{
		Response.Invoke();
	}
}
