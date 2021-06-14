using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteInEditMode]
public class BuildMenuButton : MonoBehaviour
{
	public BuildingTable BuildingTable;
	public BuildMenu BuildMenu;

	public Button Button;
	public Image Image;
	public int BuildingID;

	private void Update() 
	{
		Button.onClick.AddListener(() => { BuildMenu.Build(BuildingID); });
		Image.sprite = BuildingTable.GetById(BuildingID).Icon;
	}

}
