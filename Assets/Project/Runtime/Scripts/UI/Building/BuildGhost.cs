using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGhost : MonoBehaviour
{
	[Header("References")]
	public SettingsInjecter SettingsInjecter;
	public PlayerMaterials PlayerMaterials;
	public GameObject BuildingContainer;

	private SpriteRenderer sr;
	private Building currentGhost;

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
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

		SettingsInjecter.MapSettings.GetTile(TileConversion.WorldToTile(worldPoint)).ContainedObjects.Add(building);

		List<TileTravelType> tileTT = SettingsInjecter.MapSettings.GetTile(TileConversion.WorldToTile(worldPoint)).TravelType;
		if (!tileTT.Contains(currentGhost.TravelType))
		{
			SettingsInjecter.MapSettings.GetTile(TileConversion.WorldToTile(worldPoint)).TravelType.Add(currentGhost.TravelType);
		}
	}

	private void HandleBuildingCost()
	{

	}

	private bool CanAffordBuilding(int buildingID)
	{
		foreach (ItemCount cost in SettingsInjecter.ItemTable.GetById(buildingID).Recipe.ItemCosts)
		{
			if (PlayerMaterials.GetValue(buildingID) < cost.Count) { return false;}
		}

		return true;
	}

	public void Enable() => gameObject.SetActive(true);
	public void Disable() => gameObject.SetActive(false);
}