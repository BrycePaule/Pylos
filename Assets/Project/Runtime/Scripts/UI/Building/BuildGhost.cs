using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGhost : MonoBehaviour
{
	public GameObject BuildingPrefab;
	public GameObject BuildingContainer;

	private SpriteRenderer sr;
	private BuildingTableEntry buildingGhost;

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

	public void UpdateCurrentGhost(BuildingTableEntry building)
	{
		if (buildingGhost == building) { return; }

		buildingGhost = building;
		sr.sprite = building.Sprite;
	}

	public void Build(Vector3 mpos)
	{
		Ray ray = Camera.main.ScreenPointToRay(mpos);
		Vector3 worldPoint = ray.GetPoint(0);

		GameObject building = Instantiate(BuildingPrefab, TileConversion.TileToWorld3D(TileConversion.WorldToTile(worldPoint)), Quaternion.identity, BuildingContainer.transform);
		building.name = buildingGhost.Name;

		BuildingBase bBase = building.GetComponent<BuildingBase>();
		bBase.TileLoc = TileConversion.WorldToTile(worldPoint);
		bBase.Faction = Faction.Pylos;
		bBase.ID = buildingGhost.ID;

		SpriteRenderer bSR = building.GetComponent<SpriteRenderer>();
		bSR.sprite = buildingGhost.Sprite;
	}


	public void Enable() => gameObject.SetActive(true);
	public void Disable() => gameObject.SetActive(false);
}