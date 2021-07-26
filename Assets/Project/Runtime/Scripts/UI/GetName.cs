using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetName : MonoBehaviour
{
	private void OnValidate()
	{
		gameObject.GetComponent<TMPro.TMP_Text>().text = transform.parent.name;
	}
}
