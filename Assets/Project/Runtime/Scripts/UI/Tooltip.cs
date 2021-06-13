using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class Tooltip : MonoBehaviour
{
	public PlayerSelections PlayerSelections;

	[SerializeField] private GameObject container;
	[SerializeField] private TMP_Text heading;
	[SerializeField] private Image entitySprite;
	[SerializeField] private GameObject healthBar;
	[SerializeField] private HealthBarValues healthBarValues;
	[SerializeField] private GameObject infoBox;

	[SerializeField] private GameObject PropertyElementPrefab;
	
	private Slider slider;
	private CanvasRenderer canvasRenderer;

	private List<PropertyElement> propPool = new List<PropertyElement>();
	private RectTransform tooltipRectTransform;
	private float tooltipBaseHeight;
	private float propHeight;

	private void Awake() 
	{
		slider = healthBar.GetComponentInChildren<Slider>();
		canvasRenderer = gameObject.GetComponent<CanvasRenderer>();
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
		if (PlayerSelections.SelectedObjects.Count > 0 )
		{
			UpdateTooltipDisplay();
			EnableTooltip();
		}
		else if (PlayerSelections.HoveredObjects.Count > 0)
		{
			UpdateTooltipDisplay();
			EnableTooltip();
		}
		else
		{
			DisableTooltip();
		}
	}

	// DISPLAY
	private void UpdateTooltipDisplay()
	{
		GameObject obj;
		if (PlayerSelections.SelectedObjects.Count > 0)
		{
			obj = PlayerSelections.SelectedObjects[0];
		}
		else
		{
			obj = PlayerSelections.HoveredObjects[0];
		}

		UpdateHeader(obj);
		UpdateHealthBar(obj);

		if (obj.GetComponent<Item>())
		{
			Item item = obj.GetComponent<Item>();
			BuildDisplayProperties<Item>(obj, item);
			UpdateInfoBoxSize<Item>(obj);
			return;
		}

		if (obj.GetComponent<NPCBase>())
		{
			NPCBase npcBase = obj.GetComponent<NPCBase>();
			BuildDisplayProperties<NPCBase>(obj, npcBase);
			UpdateInfoBoxSize<NPCBase>(obj);
			return;
		}

		DisableAllProperties();

	}

	private void UpdateInfoBoxSize<T>(GameObject obj)
	{
		tooltipRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tooltipBaseHeight + ((healthBar.activeInHierarchy ? 0 : 1) * -10) + (typeof(T).GetFields().Length * propHeight));
	}

	private void BuildDisplayProperties<T>(GameObject obj, T comp)
	{
		int fieldCount = typeof(T).GetFields().Length;

		// create missing property elements (if needed)
		int missingProps = fieldCount - propPool.Count;
		for (int i = 0; i < missingProps; i++)
		{
			propPool.Add(Instantiate(PropertyElementPrefab, new Vector3(0, 0, 0), Quaternion.identity, infoBox.transform).GetComponent<PropertyElement>());
		}

		// disable unused property elements
		for (int i = fieldCount; i < propPool.Count; i++)
		{
			propPool[i].gameObject.SetActive(false);
		}

		// update and enable relevant fields
		FieldInfo[] fields = typeof(T).GetFields();
		for (int i = 0; i < fieldCount; i++)
		{
			propPool[i].propertyText = fields[i].Name;
			propPool[i].valueText = fields[i].GetValue(comp).ToString();
			propPool[i].gameObject.SetActive(true);
		}
	}

	private void DisableAllProperties()
	{
		for (int i = 0; i < infoBox.transform.childCount; i++)
		{
			infoBox.transform.GetChild(i).gameObject.SetActive(false);
		}

		tooltipRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tooltipBaseHeight + ((healthBar.activeInHierarchy ? 0 : 1) * -10));
	}

	private void UpdateHealthBar(GameObject obj)
	{
		if (obj.GetComponentInChildren<Health>() != null)
		{
			healthBar.SetActive(true);
			Health objHealth = obj.GetComponentInChildren<Health>();
			slider.value = objHealth.CurrentHealth / objHealth.MaxHealth;
			healthBarValues.CurrentHealth = objHealth.CurrentHealth;
			healthBarValues.MaxHealth = objHealth.MaxHealth;
		}
		else
		{
			healthBar.SetActive(false);
		}
	}

	private void UpdateHeader(GameObject obj)
	{
		heading.text = obj.name;
		SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
		entitySprite.sprite = sr.sprite;
		entitySprite.color = sr.color;
	}

	public void EnableTooltip() 
	{ 
		container.SetActive(true);
	}

	public void DisableTooltip()
	{
		container.SetActive(false);
	}

}
