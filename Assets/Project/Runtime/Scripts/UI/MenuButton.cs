using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuButton : MonoBehaviour
{
	[Header("References")]
	public TMPro.TMP_Text Title;
	public Button Button;
	public Image Icon;

	[Header("Settings")]
	public string TitleText;
	public Color ButtonClickColor;
	public Sprite IconSprite;

	private void OnValidate()
	{
		this.gameObject.name = TitleText;
		Title.text = TitleText;
		Icon.sprite = IconSprite;

		ColorBlock cb = Button.colors;
		cb.pressedColor = ButtonClickColor;
		Button.colors = cb;
	}
}
