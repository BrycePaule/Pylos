using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data Packs/Location Markers")]
public class LocationMarkers : ScriptableObject
{
	public Vector3[] Locations = new Vector3[10];
}

