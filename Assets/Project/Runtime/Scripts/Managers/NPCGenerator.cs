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
	public int OverrideAssetUpdateDelay;

	private float spawnCheckTimer;
	private float overrideAssetUpdateTimer;
	private Dictionary<NPCType, NPCData> npcDataLookup = new Dictionary<NPCType, NPCData>();
	private Dictionary<NPCType, NPCData> npcOverrideDataLookup = new Dictionary<NPCType, NPCData>();

	private void Awake() 
	{
		BuildAssetDictionary();

		if (SettingsInjecter.NPCSettings.OverrideNPCMoveDelays)
			BuildOverrideAssetDictionary();
	}

	private void Start() 
	{
		NPCSettings settings = SettingsInjecter.NPCSettings;
		if (settings.OverrideSpawnCaps || settings.OverrideNPCTypes || settings.OverrideNPCMoveDelays)
		{
			foreach (NPCType npc in System.Enum.GetValues(typeof(NPCType)))
			{
				if (settings.OverrideNPCTypes)
					if (npc != settings.NPCType)
						continue;

				Transform container = NPCContainer.Find(npc.ToString() + " Container");

				int count = npcDataLookup[npc].SpawnCount;
				if (settings.OverrideSpawnCaps)
					count = settings.SpawnCap;

				Spawn(npc, count, container);
			}
			return;
		}
	
		foreach (NPCType npc in System.Enum.GetValues(typeof(NPCType)))
		{
			Transform container = NPCContainer.Find(npc.ToString() + " Container");
			Spawn(npc, npcDataLookup[npc].SpawnCount, container);
		}
	}

	private void FixedUpdate() 
	{
		if (SettingsInjecter.NPCSettings.OverrideNPCMoveDelays)
		{
			overrideAssetUpdateTimer += Time.deltaTime;
			if (overrideAssetUpdateTimer >= OverrideAssetUpdateDelay) 
			{
				foreach (NPCData asset in npcOverrideDataLookup.Values)
				{
					asset.MoveDelay = SettingsInjecter.NPCSettings.MoveDelay;
				}
				overrideAssetUpdateTimer = 0f;
			}
		}

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
					if (container.transform.childCount < npcDataLookup[npc].SpawnCount)
					{
						Spawn(npc, npcDataLookup[npc].SpawnCount - container.transform.childCount, container);
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
			GameObject newNPC = Instantiate(npcDataLookup[npc].Prefab, Vector3.zero, Quaternion.identity, container);

			NPCData dataAsset = npcDataLookup[npc];
			if (SettingsInjecter.NPCSettings.OverrideNPCMoveDelays)
				dataAsset = npcOverrideDataLookup[npc];

			InitialiseNPC(newNPC, npc, dataAsset);
		}
	}

	private void InitialiseNPC(GameObject npcObj, NPCType npcType, NPCData statAsset)
	{
		NPCBase npcBase = npcObj.GetComponentInChildren<NPCBase>();

		Movement npcMovement = (Movement) npcBase.GetNPCComponent(NPCComponentType.Movement);
		Combat npcCombat = (Combat) npcBase.GetNPCComponent(NPCComponentType.Combat);
		Health npcHealth = (Health) npcBase.GetNPCComponent(NPCComponentType.Health);

		npcObj.name = npcType.ToString();

		Vector2Int loc = MapBoard.Instance.SelectRandomLocation(statAsset.TravelTypes);
		npcObj.transform.position = TileConversion.TileToWorld3D(loc);

		npcBase.Faction = statAsset.Faction;
		npcBase.NPCStatAsset = statAsset;

		npcMovement.TileLoc = loc;
		// MapBoard.Instance.GetTile(loc).TravelTypes.Add(TileTravelType.Impassable);
	}

	private void BuildAssetDictionary()
	{
		foreach (NPCData asset in NPCDataAssets)
		{
			npcDataLookup.Add(asset.NPCType, asset);
		}
	}

	private void BuildOverrideAssetDictionary()
	{
		foreach (var item in npcDataLookup)
		{
			NPCData newAsset = Instantiate(item.Value);
			newAsset.MoveDelay = SettingsInjecter.NPCSettings.MoveDelay;

			npcOverrideDataLookup.Add(item.Key, newAsset);
		}
	}

}
