using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
	public List<TabButton> TabButtons;

	public Color TabIdle;
	public Color TabHover;
	public Color TabActive;

	public TabButton SelectedTab;

	public void Awake()
	{
		TabButtons = new List<TabButton>();
		foreach (TabButton button in GetComponentsInChildren<TabButton>())
		{
			TabButtons.Add(button);
		}
	}

	public void Subscribe(TabButton button)
	{
		TabButtons.Add(button);
	}

	public void OnTabEnter(TabButton button)
	{
		ResetTabs();
		if (SelectedTab == null || button != SelectedTab)
		{
			button.Background.color = TabHover;
		}
	}

	public void OnTabExit(TabButton button)
	{
		ResetTabs();
	}

	public void OnTabSelected(TabButton button)
	{
		SelectedTab = button;
		ResetTabs();
		button.Background.color = TabActive;
	}

	public void ResetTabs()
	{
		foreach (TabButton button in TabButtons)
		{
			if (button == SelectedTab) { continue; }
			button.Background.color = TabIdle;
			button.Menu.SetActive(false);
		}
	}

}
