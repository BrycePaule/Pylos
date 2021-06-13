using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
	public SettingsInjecter SettingsInjecter;
	public PlayerSelections PlayerSelections;

	[SerializeField] private CameraController CameraController;
	[SerializeField] private Canvas UICanvas;
	[SerializeField] private GameObject npcContainer;

	public int yUp;
	public int yDown;
	public float easeDuration;
	public LeanTweenType easeType;

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
			TweenUpMenu();
		}
		else
		{
			TweenDownMenu();
		}
	}

	private void TweenDownMenu()
	{
		tweening = true;
		SettingsInjecter.GameSettings.MenuIsOpen = true;
		gameObject.SetActive(true);
		LeanTween.moveLocalY(gameObject, yDown, easeDuration).setEase(easeType).setOnComplete(FinishDownTween);
	}

	private void TweenUpMenu()
	{
		tweening = true;
		LeanTween.moveLocalY(gameObject, yUp, easeDuration).setEase(easeType).setOnComplete(FinishUpTween);
		SettingsInjecter.GameSettings.MenuIsOpen = false;
	}

	private void FinishDownTween()
	{
		tweening = false;
	}

	private void FinishUpTween()
	{
		tweening = false;
		gameObject.SetActive(false);
	}

	// BUTTON CALLBACKS

	public void TogglePaths() => SettingsInjecter.GameSettings.ShowPaths = !SettingsInjecter.GameSettings.ShowPaths;
	public void ToggleDetectionRadiusWireframes() => SettingsInjecter.GameSettings.ShowDetectionRadius = !SettingsInjecter.GameSettings.ShowDetectionRadius;

	public void HealthDown()
	{
		foreach (Health health in npcContainer.GetComponentsInChildren<Health>())
		{
			health.Damage(1, null);
		}
	}

	public void HealthUp()
	{
		foreach (Health health in npcContainer.GetComponentsInChildren<Health>())
		{
			health.Heal(1);
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
	
	private void SearchFor(ItemID id)
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
