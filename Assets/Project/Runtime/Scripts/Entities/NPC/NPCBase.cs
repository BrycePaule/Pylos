using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBase : MonoBehaviour
{
	public NPCType NPCType;
	public bool CanWalk;
	public bool CanSwim;
	public Faction Faction;
	public int Wood;
	public List<TileTravelType> TravelTypes;
}
