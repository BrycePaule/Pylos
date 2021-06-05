using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class Tooltip : MonoBehaviour
{
	[SerializeField] private GameObject tooltipContainer;
	[SerializeField] private TMP_Text heading;
	[SerializeField] private Image entitySprite;
	[SerializeField] private GameObject healthBar;
	[SerializeField] private GameObject infoBox;

	[SerializeField] private GameObject PropertyElementPrefab;
	
	private Slider slider;

	public GameObject SelectedObject = null;
	private GameObject _hoveredObject = null;

	private List<PropertyElement> propPool = new List<PropertyElement>();
	private RectTransform tooltipRectTransform;
	private float tooltipBaseHeight;
	private float propHeight;

	private void Awake() 
	{
		slider = healthBar.GetComponentInChildren<Slider>();
		tooltipRectTransform = gameObject.GetComponent<RectTransform>();
		tooltipBaseHeight = tooltipRectTransform.rect.height;
		propHeight = PropertyElementPrefab.GetComponent<LayoutElement>().preferredHeight;
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
		// create missing props (if needed)
		int missingProps = typeof(Item).GetFields().Length - propPool.Count;
		for (int i = 0; i < missingProps; i++)
		{
			propPool.Add(Instantiate(PropertyElementPrefab, new Vector3(0, 0, 0), Quaternion.identity, infoBox.transform).GetComponent<PropertyElement>());
		}
		
		// disable unused props
		int childCount = infoBox.transform.childCount;
		for (int i = typeof(Item).GetFields().Length; i < childCount; i++)
		{
			infoBox.transform.GetChild(i).gameObject.SetActive(false);
		}

		// update header
		heading.text = obj.name;
		SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
		entitySprite.sprite = sr.sprite;
		entitySprite.color = sr.color;

		// update health bar
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

		// update infobox size
		tooltipRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tooltipBaseHeight + ((healthBar.activeInHierarchy ? 0 : 1 ) * -10) + (typeof(Item).GetFields().Length * propHeight));

		Item item;
		if (obj.TryGetComponent<Item>(out item))
		{
			FieldInfo[] fields = item.GetType().GetFields();

			int count = 0;
			foreach (FieldInfo field in fields)
			{
				propPool[count].propertyText = field.Name;
				propPool[count].valueText = field.GetValue(item).ToString();
				count++;
			}
		}

		// NPCBase npcBase;
		// if (obj.TryGetComponent<NPCBase>(out npcBase))
		// {
		// 	GameObject obj1 = Instantiate(PropertyElementPrefab, new Vector3(0, 0, 0), Quaternion.identity, infoBox.transform);
		// 	PropertyElement prop1 = obj1.GetComponent<PropertyElement>();
		// 	prop1.propertyText = npcBase.ToString();
		// }
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
