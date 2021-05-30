using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[ExecuteAlways]
public class MenuButton : MonoBehaviour
{
	[Header("Init")]
	public TMPro.TMP_Text Title;
	public Button Button;
	public Image Icon;

	[Header("Settings")]
	public string TitleText;
	public Color TitleTextColor;
	public Color ButtonClickColor;
	public Sprite IconSprite;

	private void Update()
	{
		this.gameObject.name = TitleText;
		Title.text = TitleText;
		Title.color = TitleTextColor;
		Icon.sprite = IconSprite;

		ColorBlock cb = Button.colors;
		cb.pressedColor = ButtonClickColor;
		Button.colors = cb;
	}
}
