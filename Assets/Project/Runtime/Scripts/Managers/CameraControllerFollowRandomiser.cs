using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerFollowRandomiser : MonoBehaviour
{
	[Header("References")]
	public Cinemachine.CinemachineVirtualCamera cam;
	public Transform NPCContainer;

	[Header("Settings")]
	public float SwitchDelay;
	public bool RandomiseDelayTime;
	public float MinRandomDelay;
	public float MaxRandomDelay;

	private float switchTimer;
	private float randomSwitchDelay;

	private void Update()
	{
		switchTimer += Time.deltaTime;

		if (cam.Follow == null) { RandomiseTarget(); }

		if (RandomiseDelayTime && switchTimer >= randomSwitchDelay)
		{
			RandomiseTarget();
			RandomiseDelay();
		}
		else if (switchTimer >= SwitchDelay)
		{
			RandomiseTarget();
		}
	}

	private void RandomiseTarget()
	{
		NPCBase[] npcs = NPCContainer.GetComponentsInChildren<NPCBase>();
		cam.Follow = npcs[Random.Range(0, npcs.Length - 1)].transform;
		switchTimer = 0;
	}

	private void RandomiseDelay()
	{
		randomSwitchDelay = Random.Range(MinRandomDelay, MaxRandomDelay);
	}


}
