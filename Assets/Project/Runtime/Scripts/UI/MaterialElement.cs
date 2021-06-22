using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialElement : MonoBehaviour
{
	public PlayerMaterials PlayerMaterials;

	public TMPro.TMP_Text ValueText;
	public int ID;

	private void FixedUpdate() 
	{
		ValueText.text = PlayerMaterials.GetValue(ID).ToString();
	}
}
