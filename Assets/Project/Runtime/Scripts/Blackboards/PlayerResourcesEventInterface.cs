using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourcesEventInterface : MonoBehaviour
{
	public void OnGatherEvent(int ID, int count)
	{
		PlayerResourcesBoard.Instance.Increment(ID, count);
	}
}
