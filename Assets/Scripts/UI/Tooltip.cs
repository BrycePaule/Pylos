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

	public void Hover(Vector3 mpos)
	{
		if (_selectedObject != null) { return; }

		Ray ray = Camera.main.ScreenPointToRay(mpos);
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f);

		if (!hit.collider) { return; }

		if (hit.collider.gameObject.GetComponentInChildren<SelectableObject>()) 
		{
		   	UpdateHoveredObject(hit.collider.gameObject);
		}
		else
		{
			UpdateHoveredObject(null);
		}
	}
}


