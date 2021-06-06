using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialElement : MonoBehaviour
{
	public PlayerMaterials PlayerMaterials;

	public TMPro.TMP_Text ValueText;
	public ItemID ID;

	private void FixedUpdate() 
	{
		ValueText.text = typeof(PlayerMaterials).GetField(ID.ToString()).GetValue(PlayerMaterials).ToString();
	}
}
