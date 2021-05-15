using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
	public bool IsActive;

	[SerializeField] private GameObject tooltipContainer;
	[SerializeField] private TMP_Text heading;

	private GameObject _selectedObject;
	private GameObject _hoveredObject;

	private void Start() 
	{
		DisableTooltip();	
	}

	public void UpdateTooltip(GameObject obj)
	{
		heading.text = obj.name;
	}

	public void UpdateSelectedObject(GameObject newObject)
	{
		_selectedObject = newObject;
		if (newObject == null)
		{
			DisableTooltip();
		}
		else
		{
			UpdateTooltip(newObject);
			EnableTooltip();
		}
	}	    

	public void UpdateHoveredObject(GameObject newObject)
	{
		_hoveredObject = newObject;
		if (newObject == null)
		{
			DisableTooltip();
		}
		else
		{
			UpdateTooltip(newObject);
			EnableTooltip();
		}
	}	
	
	public void EnableTooltip()
	{
		IsActive = true;
		tooltipContainer.SetActive(true);
	}

	public void DisableTooltip()
	{
		IsActive = false;
		tooltipContainer.SetActive(false);
	}

	public void Hover(GameObject obj)
	{
		if (_selectedObject != null) { return; }

		UpdateHoveredObject(obj);
		if (obj == null) { return; }


		int hitLayer = obj.layer;
		if (hitLayer == Layer.NPC.GetHashCode() || hitLayer == Layer.Objects.GetHashCode() || hitLayer == Layer.Player.GetHashCode())
		{
			UpdateHoveredObject(obj);
			return;
		}
		else
		{
			UpdateHoveredObject(null);
		}
	}

	public void Select(GameObject obj)
	{
		if (obj == null) 
		{
			UpdateSelectedObject(null);
			return;
		}

		int hitLayer = obj.layer;
		if (hitLayer == Layer.NPC.GetHashCode() || hitLayer == Layer.Objects.GetHashCode() || hitLayer == Layer.Player.GetHashCode())
		{
			UpdateSelectedObject(obj);
		}
		else
		{
			UpdateSelectedObject(null);
		}
	}
}


