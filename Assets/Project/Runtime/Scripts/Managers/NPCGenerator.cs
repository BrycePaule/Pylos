using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NPCGenerator : MonoBehaviour
{
	[Header("References")]
	public SettingsInjecter SettingsInjecter;
	public MapGenerator MapGenerator;
	public Transform NPCContainer;
	public List<ScriptableObject> NPCDataAssets;

	[Header("Settings")]
	public int SpawnCheckDelay;

	private float spawnCheckTimer;
	private Dictionary<NPCType, NPCData> npcData = new Dictionary<NPCType, NPCData>();

	private void Awake() 
	{
		BuildNPCDictionary();
	}

	private void Start() 
	{
		NPCSettings settings = SettingsInjecter.NPCSettings;
		if (settings.OverrideSpawnCaps || settings.OverrideNPCTypes || settings.OverrideNPCSpeeds)
		{
			foreach (NPCType npc in System.Enum.GetValues(typeof(NPCType)))
			{
				if (settings.OverrideNPCTypes)
					if (npc != settings.NPCType)
						continue;

				Transform container = NPCContainer.Find(npc.ToString() + " Container");

				int count = npcData[npc].SpawnCount;
				if (settings.OverrideSpawnCaps)
					count = settings.SpawnCap;

				Spawn(npc, count, container);
			}
			return;
		}
	
		foreach (NPCType npc in System.Enum.GetValues(typeof(NPCType)))
		{
			Transform container = NPCContainer.Find(npc.ToString() + " Container");
			Spawn(npc, npcData[npc].SpawnCount, container);
		}
	}

	private void FixedUpdate() 
	{
		if (SettingsInjecter.NPCSettings.OverrideSpawnCaps || SettingsInjecter.NPCSettings.OverrideNPCTypes)
			return;

		spawnCheckTimer += Time.deltaTime;
		if (spawnCheckTimer >= SpawnCheckDelay)
		{
			foreach (NPCType npc in System.Enum.GetValues(typeof(NPCType)))
			{
				Transform container = NPCContainer.Find(npc.ToString() + " Container");
				if (container)
				{
					if (container.transform.childCount < npcData[npc].SpawnCount)
					{
						Spawn(npc, npcData[npc].SpawnCount - container.transform.childCount, container);
					}
				}
			}
			spawnCheckTimer = 0;
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

		Vector2Int loc = SettingsInjecter.MapSettings.SelectRandomLocation(npcMovement.TravelTypes);
		npcObj.transform.position = TileConversion.TileToWorld3D(loc);

		npcBase.Faction = _data.Faction;

		npcMovement.TileLoc = loc;
		npcMovement.MoveDelay = _data.MoveDelay;
		npcMovement.TilesPerStep = _data.TilesPerStep;
		npcMovement.TravelTypes = _data.TravelTypes;
		npcMovement.MeanderRange = _data.MeanderRange;
		npcMovement.SearchRange = _data.SearchRange;
		npcMovement.MovementState = new Meander(npcMovement);

		npcCombat.Damage = _data.Damage;
		npcCombat.AttackSpeed = _data.AttackSpeed;
		npcCombat.AttackRange = _data.AttackRange;

		npcHealth.MaxHealth = _data.MaxHealth;

		if (SettingsInjecter.NPCSettings.OverrideNPCSpeeds) { npcMovement.MoveDelay = SettingsInjecter.NPCSettings.MoveDelay; }
	}

	private void BuildNPCDictionary()
	{
		foreach (NPCData asset in NPCDataAssets)
		{
			npcData.Add(asset.NPCType, asset);
		}
	}

}
