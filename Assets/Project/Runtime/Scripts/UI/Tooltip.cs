using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class Tooltip : MonoBehaviour
{
	[Header("References")]
	public PlayerSelections PlayerSelections;

	public GameObject TooltipContainer;
	public TMP_Text HeaderText;
	public Image Icon;
	public GameObject HealthBar;
	public HealthBarValues HealthBarValues;
	public GameObject InfoBox;

	public GameObject PropertyElementPrefab;
	
	private Slider slider;
	private CanvasRenderer canvasRenderer;

	private List<PropertyElement> propPool = new List<PropertyElement>();
	private RectTransform tooltipRectTransform;
	private float tooltipBaseHeight;
	private float propHeight;

	private void Awake() 
	{
		slider = HealthBar.GetComponentInChildren<Slider>();
		canvasRenderer = gameObject.GetComponent<CanvasRenderer>();
		tooltipRectTransform = gameObject.GetComponent<RectTransform>();
		tooltipBaseHeight = tooltipRectTransform.rect.height;
		propHeight = PropertyElementPrefab.GetComponent<LayoutElement>().preferredHeight;
	}

	private void Start() 
	{
		PlayerSelections.DeselectAll();
		DisableTooltip();
	}

	public void Update()
	{
		if (PlayerSelections.SelectedObjects.Count > 0 )
		{
			EnableTooltip();
			UpdateTooltipDisplay();
		}
		else if (PlayerSelections.HoveredObjects.Count > 0)
		{
			EnableTooltip();
			UpdateTooltipDisplay();
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

		if (obj == null)
		{
			DisableTooltip();
			return; 
		}

		UpdateHeader(obj);
		UpdateHealthBar(obj);

		// if (obj.GetComponent<Item>())
		// {
		// 	Item item = obj.GetComponent<Item>();
		// 	BuildDisplayProperties<Item>(obj, item);
		// 	UpdateInfoBoxSize<Item>(obj);
		// 	return;
		// }

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
		tooltipRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tooltipBaseHeight + ((HealthBar.activeInHierarchy ? 0 : 1) * -10) + (typeof(T).GetFields().Length * propHeight));
	}

	private void BuildDisplayProperties<T>(GameObject obj, T comp)
	{
		int fieldCount = typeof(T).GetFields().Length;

		// create missing property elements (if needed)
		int missingProps = fieldCount - propPool.Count;
		for (int i = 0; i < missingProps; i++)
		{
			propPool.Add(Instantiate(PropertyElementPrefab, new Vector3(0, 0, 0), Quaternion.identity, InfoBox.transform).GetComponent<PropertyElement>());
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
		for (int i = 0; i < InfoBox.transform.childCount; i++)
		{
			InfoBox.transform.GetChild(i).gameObject.SetActive(false);
		}

		tooltipRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tooltipBaseHeight + ((HealthBar.activeInHierarchy ? 0 : 1) * -10));
	}

	private void UpdateHealthBar(GameObject obj)
	{
		if (obj.GetComponentInChildren<Health>() != null)
		{
			HealthBar.SetActive(true);
			Health objHealth = obj.GetComponentInChildren<Health>();
			slider.value = objHealth.CurrentHealth / objHealth.MaxHealth;
			HealthBarValues.CurrentHealth = objHealth.CurrentHealth;
			HealthBarValues.MaxHealth = objHealth.MaxHealth;
		}
		else
		{
			HealthBar.SetActive(false);
		}
	}

	private void UpdateHeader(GameObject obj)
	{
		HeaderText.text = obj.name;
		SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
		Icon.sprite = sr.sprite;
		Icon.color = sr.color;
	}

	public void EnableTooltip() 
	{ 
		TooltipContainer.SetActive(true);
	}

	public void DisableTooltip()
	{
		TooltipContainer.SetActive(false);
	}

}
