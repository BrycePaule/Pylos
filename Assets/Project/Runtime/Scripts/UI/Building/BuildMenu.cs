using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
	[Header("References")]
	public SettingsInjecter SettingsInjecter;
	public PlayerSelections PlayerSelections;
	public BuildingTable BuildingTable;
	public BuildGhost BuildGhost;

	[Header("Settings")]
	public int xOut;
	public int xIn;
	public float EaseDuration;
	public LeanTweenType EaseType;

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
		LeanTween.moveLocalX(gameObject, xIn, EaseDuration).setEase(EaseType).setOnComplete(FinishInTween);
	}

	public void TweenOutMenu()
	{
		tweening = true;
		LeanTween.moveLocalX(gameObject, xOut, EaseDuration).setEase(EaseType).setOnComplete(FinishOutTween);
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
		BuildingAsset building = BuildingTable.GetById(id);

		if (building != null)
		{
			BuildGhost.Enable();
			BuildGhost.UpdateCurrentGhost(building);
		}
	}

	// SEARCHING
	public void SearchFor(int id)
	{
		if (PlayerSelections.SelectedObjects.Count == 0) { return; }
		if (PlayerSelections.SelectedObjects[0].layer != Layer.NPC.GetHashCode()) { return; }

		Movement npcMovement = PlayerSelections.SelectedObjects[0].GetComponentInChildren<Movement>();
		npcMovement.SearchItemID = id;
		npcMovement.SetMovementState(new Search(npcMovement));
	}

	public void SearchForWood() => SearchFor(1);
	public void SearchForStone() => SearchFor(2);
	public void CancelSearch() => SearchFor(0);
}
