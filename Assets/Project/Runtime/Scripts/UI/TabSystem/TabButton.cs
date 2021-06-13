using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
	public TabGroup TabGroup;
	public Image Background;
	public GameObject Menu;

	private void Awake()
	{
		Background = GetComponent<Image>();
		Menu.SetActive(false);
	}

	private void Start()
	{
		TabGroup.Subscribe(this);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		TabGroup.OnTabSelected(this);
		Menu.SetActive(true);
	}

	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		TabGroup.OnTabEnter(this);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		TabGroup.OnTabExit(this);
	}

}
