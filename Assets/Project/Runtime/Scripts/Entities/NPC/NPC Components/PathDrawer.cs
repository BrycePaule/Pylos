using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawer : NPCComponentBase
{
	[Header("References")]
	public SettingsInjecter SettingsInjecter;

	private LineRenderer lineRenderer;

	private Movement npcMovement;

	protected override void Awake() 
	{
		base.Awake();
		npcBase.SubscribeComponent(NPCComponentType.PathDrawer, this);

		lineRenderer = GetComponent<LineRenderer>();
	}

	private void Start()
	{
		npcMovement = (Movement) npcBase.GetNPCComponent(NPCComponentType.Movement);
	}

	private void FixedUpdate() 
	{
		if (SettingsInjecter.GameSettings.ShowPaths)
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
			pathArray[i] = TileConversion.TileToWorld3D(path[i].Loc);
		}

		lineRenderer.positionCount = path.Count;
		lineRenderer.SetPositions(pathArray);
	}

	public void UpdateColour(Color color)
	{
		lineRenderer.startColor = color;
		lineRenderer.endColor = color;
	}
}
