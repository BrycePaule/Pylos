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

		if (obj.GetComponent<NPCBase>())
		{
			NPCBase npcBase = obj.GetComponent<NPCBase>();
			BuildDisplayProperties(npcBase);
			return;
		}
		// else if (obj.GetComponent<Container>())
		// {
		// 	Container container = obj.GetComponent<Container>();
		// 	BuildDisplayProperties(container);
		// 	return;
		// }

		DisableAllProperties();

	}

	private void UpdateInfoBoxSize(int fieldCount)
	{
		tooltipRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tooltipBaseHeight + ((HealthBar.activeInHierarchy ? 0 : 1) * -10) + (fieldCount * propHeight));
	}

	//	HEADER
	private void UpdateHeader(GameObject obj)
	{
		HeaderText.text = obj.name;
		SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
		Icon.sprite = sr.sprite;
		Icon.color = sr.color;
	}

	// PROPERTY ELEMENTS
	private void BuildDisplayProperties(NPCBase npc)
	{
		FieldInfo[] fields = new FieldInfo[6];
		fields[0] = typeof(Movement).GetField("TileLoc");
		fields[1] = typeof(NPCBase).GetField("Faction");
		fields[2] = typeof(Movement).GetField("MoveDelay");
		fields[3] = typeof(Combat).GetField("Damage");
		fields[4] = typeof(Combat).GetField("AttackRange");
		fields[5] = typeof(Combat).GetField("AttackSpeed");

		CreateMissingProperties(fields.Length);

		SetPropertyElement<Movement>(propPool[0], fields[0], (Movement) npc.GetNPCComponent(NPCComponentType.Movement));
		SetPropertyElement<NPCBase>(propPool[1], fields[1], npc);
		SetPropertyElement<Movement>(propPool[2], fields[2], (Movement) npc.GetNPCComponent(NPCComponentType.Movement));
		SetPropertyElement<Combat>(propPool[3], fields[3], (Combat) npc.GetNPCComponent(NPCComponentType.Combat));
		SetPropertyElement<Combat>(propPool[4], fields[4], (Combat) npc.GetNPCComponent(NPCComponentType.Combat));
		SetPropertyElement<Combat>(propPool[5], fields[5], (Combat) npc.GetNPCComponent(NPCComponentType.Combat));

		DisableUnusedProperties(fields.Length);
		UpdateInfoBoxSize(fields.Length);
	}

	private void BuildDisplayProperties(Container container)
	{
		print(container.items.Count);
		FieldInfo[] fields = new FieldInfo[container.items.Count + 1];
		fields[0] = typeof(Container).GetField("TileLoc");
		fields[1] = typeof(Container).GetField("Items");

		CreateMissingProperties(fields.Length);

		SetPropertyElement<Container>(propPool[0], fields[0], container);
		SetPropertyElement<Container>(propPool[1], fields[1], container);

		DisableUnusedProperties(fields.Length);
		UpdateInfoBoxSize(fields.Length);
	}

	private void SetPropertyElement<T>(PropertyElement prop, FieldInfo field, T obj)
	{
		prop.propertyText = field.Name;
		prop.valueText = field.GetValue(obj).ToString();
		prop.gameObject.SetActive(true);
	}

	private void CreateMissingProperties(int fieldCount)
	{
		int poolCount = propPool.Count;

		for (int i = 0; i < fieldCount - poolCount; i++)
		{
			propPool.Add(Instantiate(PropertyElementPrefab, new Vector3(0, 0, 0), Quaternion.identity, InfoBox.transform).GetComponent<PropertyElement>());
		}
	}

	private void DisableUnusedProperties(int fieldCount)
	{
		for (int i = fieldCount; i < propPool.Count; i++)
		{
			propPool[i].gameObject.SetActive(false);
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

	// HEALTHBAR
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


	public void EnableTooltip() 
	{ 
		TooltipContainer.SetActive(true);
	}

	public void DisableTooltip()
	{
		TooltipContainer.SetActive(false);
	}

}
