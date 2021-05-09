using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MobGenerator : MonoBehaviour
{

    [SerializeField] private int rabbitCount;
    [SerializeField] private int wolfCount;
    [SerializeField] private int goblinCount;

    [SerializeField] private GameObject mobPrefab;
    [SerializeField] private GameObject rabbitPrefab;
    [SerializeField] private GameObject wolfPrefab;
    [SerializeField] private GameObject goblinPrefab;

    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private MapManager mapManager;
    [SerializeField] private GameObject mobContainer;

    [SerializeField] private int spawnCheckDelay;

	private float _spawnCheckTimer;

	private Dictionary<MobType, int> _spawnCaps = new Dictionary<MobType, int>();
	private Dictionary<MobType, GameObject> _prefabs = new Dictionary<MobType, GameObject>();

	private void Awake() 
	{
		BuildSpawnCapsDictionary();
		BuildPrefabDictionary();
	}

	private void Start() 
	{
		foreach (MobType mob in System.Enum.GetValues(typeof(MobType)))
		{
			Transform container = new GameObject(mob.ToString() + "Container").transform;
			container.SetParent(mobContainer.transform);
			Spawn(mob, _spawnCaps[mob], container);
		}
	}

	private void FixedUpdate() 
	{
		_spawnCheckTimer += Time.deltaTime;
		if (_spawnCheckTimer > spawnCheckDelay)
		{
			foreach (MobType mob in System.Enum.GetValues(typeof(MobType)))
			{
				Transform container = mobContainer.transform.Find(mob.ToString() + "Container");
				if (container)
				{
					if (container.transform.childCount < _spawnCaps[mob])
					{
						Spawn(mob, _spawnCaps[mob] - container.transform.childCount, container);
					}
				}
				else
				{
					container = new GameObject(mob.ToString() + "Container").transform;
					container.SetParent(mobContainer.transform);
					Spawn(mob, _spawnCaps[mob], container);
				}
			}
			_spawnCheckTimer = 0;
		}
	}

	private void Spawn(MobType mob, int count, Transform container)
	{
		print("Spawning " + count + " " + mob.ToString() + "(s)");
		for (int i = 0; i < count; i++)
		{
            GameObject newMob = Instantiate(_prefabs[mob], Vector3.zero, Quaternion.identity, container);
			MobMovement mobMovement = newMob.GetComponentInChildren<MobMovement>();
			MobBase mobBase = newMob.GetComponentInChildren<MobBase>();

			newMob.name = mob.ToString();
			Vector2Int loc = SelectRandomLocation(walkable: mobBase.CanWalk, swimmable: mobBase.CanSwim);
            mobMovement.MapGenerator = mapGenerator;
            mobMovement.MapManager = mapManager;
			newMob.transform.position = TileConversion.TileToWorld3D(loc);
			mobMovement.TileLoc = loc;
			mobMovement.RandomiseMovementTick();
			mobMovement.SelectNewTargetLocation();
		}
	}

	private void BuildSpawnCapsDictionary()
	{
		_spawnCaps.Add(MobType.Rabbit, rabbitCount);
		_spawnCaps.Add(MobType.Wolf, wolfCount);
		_spawnCaps.Add(MobType.Goblin, goblinCount);

		if (_spawnCaps.Count < System.Enum.GetNames(typeof(MobType)).Length) 
		{ 
			print("Not all mobs have spawncaps!!!!!!"); 
		}
	}

	private void BuildPrefabDictionary()
	{
		_prefabs.Add(MobType.Rabbit, rabbitPrefab);
		_prefabs.Add(MobType.Wolf, wolfPrefab);
		_prefabs.Add(MobType.Goblin, goblinPrefab);

		if (_prefabs.Count < System.Enum.GetNames(typeof(MobType)).Length) 
		{ 
			print("Not all mobs have prefabs!!!!!!"); 
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
