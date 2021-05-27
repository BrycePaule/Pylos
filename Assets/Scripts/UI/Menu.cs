using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

	[SerializeField] private GameObject npcContainer;

    private void Start() 
	{
		DisableMenu();	
	}

	public void ToggleMenu()
	{
		gameObject.SetActive(!gameObject.active);
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
			health.Damage(1);
		}
	}

	public void HealthUp()
	{
		foreach (Health health in npcContainer.GetComponentsInChildren<Health>())
		{
			health.Heal(1);
		}
	}

}
