using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroList
{
	public Dictionary<NPCBase, int> targets = new Dictionary<NPCBase, int>();
	public NPCBase Highest;

	public void Remove(NPCBase npc)
	{
		if (targets.ContainsKey(npc))
		{
			targets.Remove(npc);
			UpdateHighest();
		}
	}

	public void Increment(NPCBase npc, int value = 1)
	{
		if (targets.ContainsKey(npc))
		{
			targets[npc] += value;
			UpdateHighest();
		}
		else
		{
			targets.Add(npc, value);
			UpdateHighest();
		}
	}
	
	public void Decrement(NPCBase npc, int value = 1)
	{
		if (targets.ContainsKey(npc))
		{
			if (targets[npc] > value)
			{
				targets[npc] -= value;
				UpdateHighest();
			}
			else
			{
				targets.Remove(npc);
				UpdateHighest();
			}
		}
		else
		{
			return;
		}
	}

	public void UpdateHighest()
	{
		Highest = null;
		int highestAggro = 0;

		foreach (NPCBase key in targets.Keys)
		{
			if (targets[key] > highestAggro)
			{
				highestAggro = targets[key];
				Highest = key;
			}
		}
	}

	public int Count
	{
		get 
		{
			return targets.Count;
		}
	}
}