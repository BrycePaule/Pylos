using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerFollowRandomiser : MonoBehaviour
{
	[SerializeField] private Cinemachine.CinemachineVirtualCamera cam;
	[SerializeField] private Transform NPCcontainer;

	public float SwitchDelay;
	public bool RandomiseDelayTime;
	public float MinRandomDelay;
	public float MaxRandomDelay;

	private float switchTimer;
	private float randomSwitchDelay;

	private void FixedUpdate()
	{
		print(switchTimer);
		switchTimer += Time.deltaTime;

		if (RandomiseDelayTime)
		{
			if (switchTimer >= randomSwitchDelay)
			{
				RandomiseTarget();
				RandomiseDelay();
				switchTimer = 0;
			}
			else if (switchTimer >= SwitchDelay)
			{
				RandomiseTarget();
				switchTimer = 0;
			}
		} 
	}

	private void RandomiseTarget()
	{
		NPCBase[] npcs = NPCcontainer.GetComponentsInChildren<NPCBase>();
		cam.Follow = npcs[Random.Range(0, npcs.Length - 1)].transform;
	}

	private void RandomiseDelay()
	{
		randomSwitchDelay = Random.Range(MinRandomDelay, MaxRandomDelay);
	}


}
