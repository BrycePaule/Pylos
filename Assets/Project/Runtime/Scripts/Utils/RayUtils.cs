using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RayUtils
{
	public static RaycastHit2D RaycastClick(Vector3 mpos, LayerMask layerMask)
	{
		Ray ray = Camera.main.ScreenPointToRay(mpos);
		return Physics2D.Raycast(ray.origin, ray.direction, 100f, layerMask);
	}

	public static RaycastHit2D[] RaycastALLClick(Vector3 mpos, LayerMask layerMask)
	{
		Ray ray = Camera.main.ScreenPointToRay(mpos);
		return Physics2D.RaycastAll(ray.origin, ray.direction, 100f, layerMask);
	}
}
