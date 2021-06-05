using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCComponentBase : MonoBehaviour
{
	protected NPCBase npcBase;

	protected virtual void Awake() 
	{
		npcBase = GetComponentInParent<NPCBase>();
	}
}
