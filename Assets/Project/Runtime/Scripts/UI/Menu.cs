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

	private bool menuEnabled;
	private bool cameraObjectFollow;
	private GameObject following;

	public int yUp;
	public int yDown;
	public float easeDuration;
	public LeanTweenType easeType;

	private bool tweening;

	private void Awake() 
	{
		menuEnabled = false;
	}

	// MENU ANIMATION

	public void ToggleMenu()
	{
		if (tweening) { return; }

		if (menuEnabled)
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
		menuEnabled = true;
		gameObject.SetActive(true);
		LeanTween.moveLocalY(gameObject, yDown, easeDuration).setEase(easeType).setOnComplete(FinishDownTween);
	}

	private void TweenUpMenu()
	{
		tweening = true;
		LeanTween.moveLocalY(gameObject, yUp, easeDuration).setEase(easeType).setOnComplete(FinishUpTween);
		menuEnabled = false;
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

	public void TogglePaths()
	{
		GameSettings.ShowPaths = !GameSettings.ShowPaths;
	}
	
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
		if (!Tooltip.SelectedObject) { return; }
		if (Tooltip.SelectedObject.layer != Layer.NPC.GetHashCode()) { return; }
		Movement npcMovement = Tooltip.SelectedObject.GetComponentInChildren<Movement>();
		npcMovement.searchingFor = ItemID.Wood;
		npcMovement.NPCMovementType = MovementType.Search;
	}

	public void SearchForWood() => SearchFor(ItemID.Wood); 
	public void SearchForStone() => SearchFor(ItemID.Stone); 


	// CAMERA

	private void UpdateCameraFollow()
	{
		if (Tooltip.SelectedObject == null)
		{
			cameraObjectFollow = false;
		}

		if (cameraObjectFollow)
		{
			following = Tooltip.SelectedObject;
			DefaultCamera.m_Follow = following.transform;
		}
		else
		{
			DefaultCamera.m_Follow = CameraController.transform;
		}
	}

}
