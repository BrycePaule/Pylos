using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NPCData", menuName = "NPCData/DefaultNPCData", order = 1)]
public class NPCData : ScriptableObject
{
	public NPCType NPCType;
    public GameObject Prefab;
	public int SpawnCount;
}