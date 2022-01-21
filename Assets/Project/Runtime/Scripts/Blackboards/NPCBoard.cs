using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blackboards/NPCBoard")]
public class NPCBoard : Blackboard
{
	private static NPCBoard _instance;


	public static NPCBoard Instance
	{
		get
		{
			if (!_instance)
				_instance = FindObjectOfType<NPCBoard>();
			if (!_instance)
				_instance = CreateInstance<NPCBoard>();
			return _instance;
		}
	}

	public void Initialise(int mapSize)
	{
		// Tiles = new GroundTile[mapSize, mapSize];
	}
}
