using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContainer
{
	bool Contains(ItemID id);
	int Take(ItemID id, int count);
	void Put(ItemID id, int count);
}
