using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGhost : MonoBehaviour
{
	[Header("References")]
	public SettingsInjecter SettingsInjecter;
	public GameObject BuildingContainer;

	private MapBoard MapBoard;
	private SpriteRenderer sr;
	private Building currentGhost;

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		MapBoard = MapBoard.Instance;
		Disable();
	}
	
	public void UpdateLocation(Vector3 mpos)
	{
		Ray ray = Camera.main.ScreenPointToRay(mpos);
		Vector3 worldPoint = ray.GetPoint(0);
		transform.position = new Vector3(worldPoint.x, worldPoint.y, 1);
	}

	public void UpdateCurrentGhost(Building building)
	{
		if (currentGhost == building) { return; }

		currentGhost = building;
		sr.sprite = building.Sprite;
	}

	public void Build(Vector3 mpos)
	{
		if (RayUtils.RaycastClick(mpos, SettingsInjecter.GameSettings.NonBuildableLayers)) { return; }
		if (!CanAffordBuilding(currentGhost.ID)) { return; }
		HandleBuildingCost(currentGhost.ID);

		Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mpos);
		GameObject building = Instantiate(currentGhost.Prefab, TileConversion.TileToWorld3D(TileConversion.WorldToTile(worldPoint)), Quaternion.identity, BuildingContainer.transform);
		building.name = currentGhost.Name;

		BuildingBase bBase = building.GetComponent<BuildingBase>();
		bBase.TileLoc = TileConversion.WorldToTile(worldPoint);
		bBase.Faction = Faction.Pylos;
		bBase.ID = currentGhost.ID;
		bBase.TravelType = currentGhost.TravelType;
		if (currentGhost.TravelType == TileTravelType.Impassable)
		{
			building.GetComponent<BoxCollider2D>().isTrigger = true;
		}

		SpriteRenderer bSR = building.GetComponent<SpriteRenderer>();
		bSR.sprite = currentGhost.Sprite;

		MapBoard.GetTile(TileConversion.WorldToTile(worldPoint)).ContainedObjects.Add(building);

		List<TileTravelType> tileTT = MapBoard.GetTile(TileConversion.WorldToTile(worldPoint)).TravelTypes;
		if (!tileTT.Contains(currentGhost.TravelType))
		{
			MapBoard.GetTile(TileConversion.WorldToTile(worldPoint)).TravelTypes.Add(currentGhost.TravelType);
		}
	}

	private void HandleBuildingCost(int buildingID)
	{
		foreach (ItemCount cost in SettingsInjecter.ItemTable.GetById(buildingID).Recipe.ItemCosts)
		{
			PlayerResourcesBoard.Instance.Decrement(cost.ID, cost.Count);
		}
	}

	private bool CanAffordBuilding(int buildingID)
	{
		foreach (ItemCount cost in SettingsInjecter.ItemTable.GetById(buildingID).Recipe.ItemCosts)
		{
			if (PlayerResourcesBoard.Instance.GetValue(cost.ID) < cost.Count) { return false;}
		}

		return true;
	}

	public void Enable() => gameObject.SetActive(true);
	public void Disable() => gameObject.SetActive(false);
}