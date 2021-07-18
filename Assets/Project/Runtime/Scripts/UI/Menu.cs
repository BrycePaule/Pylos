using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
	[Header("References")]
	public SettingsInjecter SettingsInjecter;
	public PlayerSelections PlayerSelections;
	public CameraController CameraController;
	public GameObject NPCContainer;

	[Header("Settings")]
	public int yUp;
	public int yDown;
	public float EaseDuration;
	public LeanTweenType EaseType;

	private bool tweening;

	private void Awake() 
	{
		SettingsInjecter.GameSettings.MenuIsOpen = false;
	}

	// MENU ANIMATION

	public void ToggleMenu()
	{
		if (tweening) { return; }

		if (SettingsInjecter.GameSettings.MenuIsOpen)
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
		SettingsInjecter.GameSettings.MenuIsOpen = true;
		gameObject.SetActive(true);
		LeanTween.moveLocalY(gameObject, yDown, EaseDuration).setEase(EaseType).setOnComplete(FinishInTween);
	}

	public void TweenOutMenu()
	{
		tweening = true;
		LeanTween.moveLocalY(gameObject, yUp, EaseDuration).setEase(EaseType).setOnComplete(FinishOutTween);
		SettingsInjecter.GameSettings.MenuIsOpen = false;
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

	public void TogglePaths() => SettingsInjecter.GameSettings.ShowPaths = !SettingsInjecter.GameSettings.ShowPaths;
	public void ToggleDetectionRadiusWireframes() => SettingsInjecter.GameSettings.ShowDetectionRadius = !SettingsInjecter.GameSettings.ShowDetectionRadius;

	public void HealthDown()
	{
		foreach (Health health in NPCContainer.GetComponentsInChildren<Health>())
		{
			health.Damage(1, null);
		}
	}

	public void HealthUp()
	{
		foreach (Health health in NPCContainer.GetComponentsInChildren<Health>())
		{
			health.Heal(1, null);
		}
	}

	public void ToggleCameraFollow()
	{
		if (PlayerSelections.SelectedObjects.Count > 0)
		{
			CameraController.SetCameraFollow(PlayerSelections.SelectedObjects[0]);
		}
		else
		{
			CameraController.SetCameraFollow(null);
		}
	}
	
	private void SearchFor(int id)
	{
		if (PlayerSelections.SelectedObjects.Count == 0) { return; }
		if (PlayerSelections.SelectedObjects[0].layer != Layer.NPC.GetHashCode()) { return; }

		Movement npcMovement = PlayerSelections.SelectedObjects[0].GetComponentInChildren<Movement>();
		npcMovement.SearchItemID = id;
		npcMovement.SetMovementState(new Search(npcMovement));
	}

	public void SearchForWood() => SearchFor(SettingsInjecter.ItemTable.GetByName("Wood").ID); 
	public void SearchForStone() => SearchFor(SettingsInjecter.ItemTable.GetByName("Stone").ID); 
	public void CancelSearch() => SearchFor(SettingsInjecter.ItemTable.GetByName("Item").ID); 

}
