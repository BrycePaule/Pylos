using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GetName : MonoBehaviour
{
	private void Update() 
	{
		gameObject.GetComponent<TMPro.TMP_Text>().text = transform.parent.name;
	}
}
