using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NPCGenerator : MonoBehaviour
{

    [SerializeField] private bool SPAWN_MOBS;
	[Space(20)]

    [SerializeField] private List<ScriptableObject> npcDataAssets;

    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private MapManager mapManager;
    [SerializeField] private GameObject npcContainer;

    [SerializeField] private int spawnCheckDelay;

	private float _spawnCheckTimer;

	private Dictionary<NPCType, NPCData> npcData = new Dictionary<NPCType, NPCData>();

	private void Awake() 
	{
		BuildNPCDictionary();
	}

	private void Start() 
	{
		foreach (NPCType npc in System.Enum.GetValues(typeof(NPCType)))
		{
			Transform container = new GameObject(npc.ToString() + "Container").transform;
			container.SetParent(npcContainer.transform);
			Spawn(npc, npcData[npc].SpawnCount, container);
		}
	}

	private void FixedUpdate() 
	{
		_spawnCheckTimer += Time.deltaTime;
		if (_spawnCheckTimer > spawnCheckDelay)
		{
			foreach (NPCType npc in System.Enum.GetValues(typeof(NPCType)))
			{
				Transform container = npcContainer.transform.Find(npc.ToString() + "Container");
				if (container)
				{
					if (container.transform.childCount < npcData[npc].SpawnCount)
					{
						Spawn(npc, npcData[npc].SpawnCount - container.transform.childCount, container);
					}
				}
				else
				{
					container = new GameObject(npc.ToString() + "Container").transform;
					container.SetParent(npcContainer.transform);
					Spawn(npc, npcData[npc].SpawnCount, container);
				}
			}
			_spawnCheckTimer = 0;
		}
	}

	private void Spawn(NPCType npc, int count, Transform container)
	{
		if (!SPAWN_MOBS) { return; }

		print("Spawning " + count + " " + npc.ToString() + "(s)");
		for (int i = 0; i < count; i++)
		{
            GameObject newNPC = Instantiate(npcData[npc].Prefab, Vector3.zero, Quaternion.identity, container);
			NPCMovement npcMovement = newNPC.GetComponentInChildren<NPCMovement>();
			NPCBase npcBase = newNPC.GetComponentInChildren<NPCBase>();

			newNPC.name = npc.ToString();
			Vector2Int loc = SelectRandomLocation(walkable: npcBase.CanWalk, swimmable: npcBase.CanSwim);
            npcMovement.MapGenerator = mapGenerator;
            npcMovement.MapManager = mapManager;
			newNPC.transform.position = TileConversion.TileToWorld3D(loc);
			npcMovement.TileLoc = loc;
			npcMovement.RandomiseMovementTick();
			npcMovement.SelectNewTargetLocation();
		}
	}

	private void BuildNPCDictionary()
	{
		foreach (NPCData asset in npcDataAssets)
		{
			npcData.Add(asset.NPCType, asset);
		}
	}

	private Vector2Int SelectRandomLocation(bool walkable, bool swimmable)
	{
		Vector2Int randomLoc = Vector2Int.zero;

		while (randomLoc == Vector2Int.zero)
		{
			Vector2Int potentialLoc = new Vector2Int(mapGenerator.RandomIntInBounds(), mapGenerator.RandomIntInBounds());

			if (walkable)
			{
				if (mapManager.GetTile(potentialLoc).IsWalkable) 
				{
					randomLoc = potentialLoc;
					break; 
				}
			}

			if (swimmable)
			{
				if (mapManager.GetTile(potentialLoc).IsSwimmable) 
				{
					randomLoc = potentialLoc;
					break;
				}
			}
		}
		return randomLoc;
	}
}
