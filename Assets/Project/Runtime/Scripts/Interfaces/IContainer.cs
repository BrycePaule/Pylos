using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContainer
{
	bool Contains(int id);
	int Take(int id, int count);
	void Put(int id, int count);
}
