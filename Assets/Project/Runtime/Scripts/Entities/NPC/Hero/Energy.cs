using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : NPCComponentBase
{
	public ClampedInt EnergyLevel;

	protected override void Awake() 
	{
		base.Awake();
		npcBase.SubscribeComponent(NPCComponentType.Energy, this);
	}
}
