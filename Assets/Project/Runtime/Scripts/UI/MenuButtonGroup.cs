using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[ExecuteInEditMode]
public class MenuButtonGroup : MonoBehaviour
{
	[Header("References")]
	public TMPro.TMP_Text Title;
	public GameObject ButtonContainer;

	[Header("Settings")]
	public string TitleText;

	private void Update()
	{
		Title.text = TitleText;
		this.gameObject.name = TitleText;
	}
}
