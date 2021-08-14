using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyElement : MonoBehaviour
{
	public TMPro.TMP_Text property;
	public TMPro.TMP_Text value;

	public string propertyText;
	public string valueText;
	
	private void OnValidate()
	{
		this.gameObject.name = propertyText;
		property.text = propertyText;
		value.text = valueText;
	}
}
