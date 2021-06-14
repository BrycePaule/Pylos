using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
	public SettingsInjecter SettingsInjecter;
	public BuildingTable BuildingTable;
	public PlayerSelections PlayerSelections;
	public BuildGhost BuildGhost;

	public int xOut;
	public int xIn;
	public float easeDuration;
	public LeanTweenType easeType;

	private bool tweening;

	private void Awake() 
	{
		SettingsInjecter.GameSettings.BuildMenuIsOpen = false;
	}

	// MENU ANIMATION
	public void ToggleBuildMenu()
	{
		if (tweening) { return; }

		if (SettingsInjecter.GameSettings.BuildMenuIsOpen)
		{
			TweenOutMenu();
		}
		else
		{
			TweenInMenu();
		}
	}

	private void TweenInMenu()
	{
		tweening = true;
		SettingsInjecter.GameSettings.BuildMenuIsOpen = true;
		gameObject.SetActive(true);
		LeanTween.moveLocalX(gameObject, xIn, easeDuration).setEase(easeType).setOnComplete(FinishInTween);
	}

	public void TweenOutMenu()
	{
		tweening = true;
		LeanTween.moveLocalX(gameObject, xOut, easeDuration).setEase(easeType).setOnComplete(FinishOutTween);
		SettingsInjecter.GameSettings.BuildMenuIsOpen = false;
	}

	private void FinishInTween()
	{
		tweening = false;
	}

	private void FinishOutTween()
	{
		tweening = false;
		gameObject.SetActive(false);
	}

	// BUTTON CALLBACKS

	// BUILDING
	public void Build(int id)
	{
		TweenOutMenu();
		SettingsInjecter.GameSettings.IsBuilding = true;
		BuildingTableEntry building = BuildingTable.GetById(id);

		if (building != null)
		{
			BuildGhost.UpdateCurrentGhost(building);
			BuildGhost.Enable();
		}
	}

	// SEARCHING
	public void SearchFor(ItemID id)
	{
		if (PlayerSelections.SelectedObjects.Count == 0) { return; }
		if (PlayerSelections.SelectedObjects[0].layer != Layer.NPC.GetHashCode()) { return; }

		Movement npcMovement = PlayerSelections.SelectedObjects[0].GetComponentInChildren<Movement>();
		npcMovement.searchingFor = id;
		npcMovement.NPCMovementType = MovementType.Search;
	}

	public void SearchForWood() => SearchFor(ItemID.Wood); 
	public void SearchForStone() => SearchFor(ItemID.Stone); 
	public void CancelSearch() => SearchFor(ItemID.Item); 
}
