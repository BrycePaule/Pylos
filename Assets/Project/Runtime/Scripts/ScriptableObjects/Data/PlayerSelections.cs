using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data Packs/Player Selections")]
public class PlayerSelections : ScriptableObject
{
	public List<GameObject> SelectedObjects;
	public List<GameObject> HoveredObjects;

	public void Hover(GameObject obj)
	{
		HoveredObjects = new List<GameObject>(); 
		
		if (obj != null)
		{
			HoveredObjects.Add(obj);
		}
	}

	public void Select(GameObject obj)
	{
		CheckMissings();
		DisableSelectionRings();
		SelectedObjects = new List<GameObject>(){obj};
		EnableSelectionRings();
	}

	public void Select(List<GameObject> objs)
	{
		CheckMissings();
		DisableSelectionRings();
		SelectedObjects = objs;
		EnableSelectionRings();
	}

	public void DeselectAll()
	{
		CheckMissings();
		DisableSelectionRings();
		HoveredObjects.Clear();
		SelectedObjects.Clear();
	}

	private void EnableSelectionRings()
	{
		foreach (GameObject obj in SelectedObjects)
		{
			obj.GetComponentInChildren<SelectionRing>().Select();
		}
	}

	private void DisableSelectionRings()
	{
		foreach (GameObject obj in SelectedObjects)
		{
			obj.GetComponentInChildren<SelectionRing>().Deselect();
		}
	}

	private void CheckMissings()
	{
		for (int i = SelectedObjects.Count - 1; i >= 0; i--)
		{
			if (SelectedObjects[i] == null) 
			{
				SelectedObjects.RemoveAt(i);
			}
		}
	}

}
