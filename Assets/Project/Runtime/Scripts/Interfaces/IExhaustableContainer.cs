using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExhaustableContainer : IContainer
{
	bool IsEmpty();
	void Exhaust();
}

