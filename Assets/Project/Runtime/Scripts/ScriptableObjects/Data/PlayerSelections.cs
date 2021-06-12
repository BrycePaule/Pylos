using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data Packs/Player Selections")]
public class PlayerSelections : ScriptableObject
{
	public List<GameObject> SelectedObjects = new List<GameObject>();
	public List<GameObject> HoveredObjects = new List<GameObject>();

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
		SelectedObjects = new List<GameObject>(){obj};
	}

	public void Select(List<GameObject> objs)
	{
		SelectedObjects = objs;
	}

	public void DeselectAll()
	{
		HoveredObjects.Clear();
		SelectedObjects.Clear();
	}

}
