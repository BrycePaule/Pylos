using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementStateMachine : MonoBehaviour
{
	public MovementState State;

	public void SetState(MovementState state)
	{
		State = state;
	}
	
}
