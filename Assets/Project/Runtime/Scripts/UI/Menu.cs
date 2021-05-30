using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

	[SerializeField] private GameObject npcContainer;

	[SerializeField] private GameObject CameraController;
	[SerializeField] private Cinemachine.CinemachineVirtualCamera DefaultCamera;
	[SerializeField] private Tooltip Tooltip;

	private bool cameraObjectFollow;
	private GameObject following;

	public void ToggleMenu()
	{
		gameObject.SetActive(!gameObject.activeInHierarchy);
	}

	private void EnableMenu()
	{
		gameObject.SetActive(true);
	}

	private void DisableMenu()
	{
		gameObject.SetActive(false);
	}

	public void TogglePaths()
	{
		foreach (NPCMovement npcMovement in npcContainer.GetComponentsInChildren<NPCMovement>())
		{
			npcMovement.ShowPath = !npcMovement.ShowPath;
		}
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
			CameraController.transform.position = following.transform.position;
			following = null;
		}

		cameraObjectFollow = !cameraObjectFollow;
		UpdateCameraFollow();
	}
	
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
