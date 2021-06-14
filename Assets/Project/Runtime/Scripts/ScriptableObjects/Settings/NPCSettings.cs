using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/NPC Settings")]
public class NPCSettings : ScriptableObject
{
	public bool OverrideSpawnCaps;
	public int SpawnCap;

	public bool OverrideNPCTypes;
	public NPCType NPCType;

	public bool OverrideNPCSpeeds;
	public float MoveDelay;
}
