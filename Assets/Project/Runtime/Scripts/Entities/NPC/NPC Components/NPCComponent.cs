using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCComponent : MonoBehaviour
{
	private NPCBase npcBase;

	private void Awake() 
	{
		npcBase = GetComponentInParent<NPCBase>();
	}
}
