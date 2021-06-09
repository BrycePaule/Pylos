using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
	public GameSettings GameSettings;

	[SerializeField] private Canvas UICanvas;

	[SerializeField] private GameObject npcContainer;
	[SerializeField] private GameObject CameraController;
	[SerializeField] private Cinemachine.CinemachineVirtualCamera DefaultCamera;
	[SerializeField] private Tooltip Tooltip;

	private bool cameraObjectFollow;
	private GameObject following;

	public int yUp;
	public int yDown;
	public float easeDuration;
	public LeanTweenType easeType;

	private bool tweening;

	private void Awake() 
	{
		GameSettings.MenuIsOpen = false;
	}

	// MENU ANIMATION

	public void ToggleMenu()
	{
		if (tweening) { return; }

		if (GameSettings.MenuIsOpen)
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
		GameSettings.MenuIsOpen = true;
		gameObject.SetActive(true);
		LeanTween.moveLocalY(gameObject, yDown, easeDuration).setEase(easeType).setOnComplete(FinishDownTween);
	}

	private void TweenUpMenu()
	{
		tweening = true;
		LeanTween.moveLocalY(gameObject, yUp, easeDuration).setEase(easeType).setOnComplete(FinishUpTween);
		GameSettings.MenuIsOpen = false;
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

	public void TogglePaths() => GameSettings.ShowPaths = !GameSettings.ShowPaths;
	public void ToggleDetectionRadiusWireframes() => GameSettings.ShowDetectionRadius = !GameSettings.ShowDetectionRadius;

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
		if (cameraObjectFollow)
		{
			if (following == null) 
			{ 
				following = null;
				return; 
			}

			CameraController.transform.position = following.transform.position;
			following = null;
		}

		cameraObjectFollow = !cameraObjectFollow;
		UpdateCameraFollow();
	}
	
	private void SearchFor(ItemID id)
	{
		if (Tooltip.SelectedObjects.Count == 0) { return; }
		if (Tooltip.SelectedObjects[0].layer != Layer.NPC.GetHashCode()) { return; }

		Movement npcMovement = Tooltip.SelectedObjects[0].GetComponentInChildren<Movement>();
		npcMovement.searchingFor = id;
		npcMovement.NPCMovementType = MovementType.Search;
	}

	public void SearchForWood() => SearchFor(ItemID.Wood); 
	public void SearchForStone() => SearchFor(ItemID.Stone); 
	public void CancelSearch() => SearchFor(ItemID.Item); 


	// CAMERA

	private void UpdateCameraFollow()
	{
		if (Tooltip.SelectedObjects.Count == 0)
		{
			cameraObjectFollow = false;
		}

		if (cameraObjectFollow)
		{
			following = Tooltip.SelectedObjects[0];
			DefaultCamera.m_Follow = following.transform;
		}
		else
		{
			DefaultCamera.m_Follow = CameraController.transform;
		}
	}

}
