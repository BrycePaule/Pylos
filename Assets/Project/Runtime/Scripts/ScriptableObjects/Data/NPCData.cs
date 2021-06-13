using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NPCData", menuName = "NPCData/DefaultNPCData", order = 1)]
public class NPCData : ScriptableObject
{
	[Header("Spawn Settings")]
	public GameObject Prefab;
	public int SpawnCount;

	[Header("Types & Groupings")]
	public NPCType NPCType;
	public Faction Faction;

	[Header("Movement")]
	public float MoveDelay;
	public int TilesPerStep;
	public int MeanderRange;
	public int SearchRange;
	
	[Header("Combat")]
	public float MaxHealth;
	public float Damage;
	public float AttackSpeed;
	public int AttackRange;

	[Header("Detection Radius")]
	public int DetectionRange;
}