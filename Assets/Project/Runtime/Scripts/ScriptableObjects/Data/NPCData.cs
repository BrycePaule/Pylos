using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New NPCData", menuName = "Data Packs/NPC Data", order = 1)]
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
	public List<TileTravelType> TravelTypes;
	public int TilesPerStep;
	public int MeanderRange;
	public int SearchRange;
	public float SearchDelay;
	
	[Header("Combat")]
	public float MaxHealth;
	public float Damage;
	public float AttackSpeed;
	public int AttackRange;

	[Header("Aggro")]
	public float AggroUpdateDelay;

	[Header("Detection Radius")]
	public int DetectionRange;

	[Header("Loot")]
	public DropTable DropTable;
}