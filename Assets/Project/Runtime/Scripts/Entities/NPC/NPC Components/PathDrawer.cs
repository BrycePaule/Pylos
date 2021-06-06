using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawer : NPCComponentBase
{
	public GameSettings GameSettings;

	private LineRenderer lineRenderer;

	private Aggro npcAggro;

	protected override void Awake() 
	{
		base.Awake();
		npcBase.SubscribeComponent(NPCComponentType.PathDrawer, this);

		lineRenderer = GetComponent<LineRenderer>();
	}

	private void Start()
	{
		npcAggro = (Aggro) npcBase.GetNPCComponent(NPCComponentType.Aggro);
	}

	private void FixedUpdate() 
	{
		if (GameSettings.ShowPaths)
		{
			lineRenderer.enabled = true;
		}
		else
		{
			lineRenderer.enabled = false;
		}
	}

	public void UpdatePath(List<Node> path)
	{
		Vector3[] pathArray = new Vector3[path.Count];
		for (int i = 0; i < path.Count; i++)
		{
			pathArray[i] = TileConversion.TileToWorld3D(path[i].GlobalLoc);
		}

		npcAggro = (Aggro) npcBase.GetNPCComponent(NPCComponentType.Aggro);
		lineRenderer.startColor = (npcAggro.IsAggro) ? Color.red : Color.cyan;
		lineRenderer.endColor = (npcAggro.IsAggro) ? Color.red : Color.cyan;

		lineRenderer.positionCount = path.Count;
		lineRenderer.SetPositions(pathArray);
	}
}
