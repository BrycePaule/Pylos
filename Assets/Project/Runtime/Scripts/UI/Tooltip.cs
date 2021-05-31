using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
	[SerializeField] private GameObject tooltipContainer;
	[SerializeField] private TMP_Text heading;
	[SerializeField] private Image entitySprite;
	[SerializeField] private GameObject healthBar;

	private Slider slider;

	public GameObject SelectedObject = null;
	private GameObject _hoveredObject = null;

	private void Awake() 
	{
		slider = healthBar.GetComponentInChildren<Slider>();
	}

	private void Start() 
	{
		DisableTooltip();	
	}

	public void FixedUpdate()
	{
		if (SelectedObject != null)
		{
			UpdateTooltip(SelectedObject);
		}
		else if ( _hoveredObject != null)
		{
			UpdateTooltip(_hoveredObject);
		}
	}

	public void UpdateTooltip(GameObject obj)
	{
		heading.text = obj.name;
		
		SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
		entitySprite.sprite = sr.sprite;
		entitySprite.color = sr.color;

		if (obj.GetComponentInChildren<Health>() != null)
		{
			healthBar.SetActive(true);
			Health objHealth = obj.GetComponentInChildren<Health>();
			slider.value = objHealth.CurrentHealth / objHealth.MaxHealth;
		}
		else
		{
			healthBar.SetActive(false);
		}
	}

	public void UpdateSelectedObject(GameObject newObject)
	{
		SelectedObject = newObject;
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
		tooltipContainer.SetActive(true);
	}

	public void DisableTooltip()
	{
		tooltipContainer.SetActive(false);
	}

	public void Hover(GameObject obj)
	{
		if (SelectedObject != null) { return; }
		UpdateHoveredObject(obj);
	}

	public void Select(GameObject obj)
	{
		UpdateSelectedObject(obj);
	}
}
