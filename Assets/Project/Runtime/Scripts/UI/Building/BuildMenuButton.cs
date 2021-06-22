using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;

[ExecuteInEditMode]
public class BuildMenuButton : MonoBehaviour
{
	[Header("References")]
	public BuildingTable BuildingTable;
	public BuildMenu BuildMenu;

	[Header("Settings")]
	public Button Button;
	public Image Image;
	public int BuildingID;

	private void Update() 
	{
		Button.onClick.RemoveAllListeners();
		Button.onClick.AddListener(() => { BuildMenu.Build(BuildingID);});
		Image.sprite = BuildingTable.GetById(BuildingID).Icon;
		gameObject.name = BuildingTable.GetById(BuildingID).Name;
	}

}
