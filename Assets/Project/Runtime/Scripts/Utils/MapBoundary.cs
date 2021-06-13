using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoundary : MonoBehaviour
{
	public SettingsInjecter SettingsInjecter;

	[SerializeField] private GameObject topBorder; 
	[SerializeField] private GameObject botBorder; 
	[SerializeField] private GameObject leftBorder; 
	[SerializeField] private GameObject rightBorder;

	private void Awake() 
	{
		int size = SettingsInjecter.MapSettings.MapSize;

		topBorder.transform.position = new Vector3(size / 2, size + 1.5f, 0);
		topBorder.transform.localScale = new Vector3(size + 6, 3, 1);

		botBorder.transform.position = new Vector3(size / 2, -1.5f, 0);
		botBorder.transform.localScale = new Vector3(size + 6, 3, 1);

		leftBorder.transform.position = new Vector3(-1.5f, size / 2, 0);
		leftBorder.transform.localScale = new Vector3(3, size + 6, 1);

		rightBorder.transform.position = new Vector3(size + 1.5f, size / 2, 0);
		rightBorder.transform.localScale = new Vector3(3, size + 6, 1);
	}

}
