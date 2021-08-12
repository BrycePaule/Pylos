using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needs : NPCComponentBase
{
	public float Hunger;
	public float Energy;

	protected override void Awake() 
	{
		base.Awake();
		npcBase.SubscribeComponent(NPCComponentType.Needs, this);
	}
}
