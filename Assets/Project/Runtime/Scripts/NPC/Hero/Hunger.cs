using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : NPCComponentBase
{
	public ClampedInt HungerLevel;

	protected override void Awake() 
	{
		base.Awake();
		npcBase.SubscribeComponent(NPCComponentType.Hunger, this);
	}

	private void FixedUpdate()
	{

	}
}
