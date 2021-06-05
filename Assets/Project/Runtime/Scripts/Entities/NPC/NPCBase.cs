using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBase : MonoBehaviour
{
	public NPCType NPCType;
	public Faction Faction;

	public List<TileTravelType> TravelTypes;
	public Dictionary<NPCComponentType, NPCComponentBase> Components = new Dictionary<NPCComponentType, NPCComponentBase>();

	public void SubscribeComponent(NPCComponentType componentType, NPCComponentBase component)
	{
		if (Components.ContainsKey(componentType))
		{
			print("NPC already has " + componentType);
			return;
		}
		else
		{
			Components.Add(componentType, component);
		}
	}
}
