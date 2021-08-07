using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeUtility : MonoBehaviour
{

	public void IncrementTimescale()
	{
		Time.timeScale += .1f;
		Debug.Log("Current timescale: " + Time.timeScale);
	}

	public void DecrementTimescale()
	{
		Time.timeScale -= .1f;
		Debug.Log("Current timescale: " + Time.timeScale);
	}
}
