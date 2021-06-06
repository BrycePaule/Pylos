using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NPCGenerator : MonoBehaviour
{
	public MapSettings MapSettings;

	[Header("DEV STUFF")]
    [SerializeField] private bool OVERRIDE_SPAWN_COUNTS;
    [SerializeField] private NPCType NPCTYPE_OVERRIDE;
    [SerializeField] private int SPAWN_CAP_OVERRIDE;
    [SerializeField] private bool OVERRIDE_SPEED;
    [SerializeField] private float MOVE_DELAY_OVERRIDE;
	[Space(20)]

    [SerializeField] private List<ScriptableObject> npcDataAssets;

    [SerializeField] private MapGenerator mapGenerator;
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
		if (OVERRIDE_SPAWN_COUNTS)
		{
			NPCType npc = NPCTYPE_OVERRIDE;

			Transform container = new GameObject(npc.ToString() + "Container").transform;
			container.SetParent(npcContainer.transform);
			Spawn(npc, SPAWN_CAP_OVERRIDE, container);

			return;
		}

		foreach (NPCType npc in System.Enum.GetValues(typeof(NPCType)))
		{
			Transform container = new GameObject(npc.ToString() + "Container").transform;
			container.SetParent(npcContainer.transform);
			Spawn(npc, npcData[npc].SpawnCount, container);
		}
	}

	private void FixedUpdate() 
	{
		if (OVERRIDE_SPAWN_COUNTS) { return;}

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
		print("Spawning " + count + " " + npc.ToString() + "(s)");
		for (int i = 0; i < count; i++)
		{
			GameObject newNPC = Instantiate(npcData[npc].Prefab, Vector3.zero, Quaternion.identity, container);
			BuildNPC(newNPC, npc);
		}
	}

	private void BuildNPC(GameObject npcObj, NPCType npcType)
	{
		NPCData _data = npcData[npcType];
		NPCBase npcBase = npcObj.GetComponentInChildren<NPCBase>();

		Movement npcMovement = (Movement) npcBase.GetNPCComponent(NPCComponentType.Movement);
		Combat npcCombat = (Combat) npcBase.GetNPCComponent(NPCComponentType.Combat);
		Health npcHealth = (Health) npcBase.GetNPCComponent(NPCComponentType.Health);

		npcObj.name = npcType.ToString();

		Vector2Int loc = MapSettings.SelectRandomLocation(npcBase.TravelTypes);
		npcObj.transform.position = TileConversion.TileToWorld3D(loc);
		npcMovement.TileLoc = loc;

		npcBase.Faction = _data.Faction;

		npcMovement.MoveDelay = _data.MoveDelay;
		npcMovement.TilesPerStep = _data.TilesPerStep;
		npcMovement.MeanderRange = _data.MeanderRange;
		npcMovement.SearchRange = _data.SearchRange;
		npcMovement.RandomiseTimers();
		npcMovement.RandomTargetLocation(npcMovement.TileLoc);

		npcCombat.Damage = _data.Damage;
		npcCombat.AttackSpeed = _data.AttackSpeed;
		npcCombat.AttackRange = _data.AttackRange;

		npcHealth.MaxHealth = _data.MaxHealth;

		if (OVERRIDE_SPEED) { npcMovement.MoveDelay = MOVE_DELAY_OVERRIDE; }
	}

	private void BuildNPCDictionary()
	{
		foreach (NPCData asset in npcDataAssets)
		{
			npcData.Add(asset.NPCType, asset);
		}
	}

}
