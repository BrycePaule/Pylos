using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/NPC Settings")]
public class NPCSettings : ScriptableObject
{
	public bool OverrideSpawnCaps;
	public int SpawnCap;

	[Space]
	public bool OverrideNPCTypes;
	public NPCType NPCType;

	[Space]
	public bool OverrideNPCMoveDelays;
	public float MoveDelay;
}
