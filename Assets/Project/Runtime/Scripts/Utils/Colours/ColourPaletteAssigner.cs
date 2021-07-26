using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourPaletteAssigner : MonoBehaviour
{
	[Header("References")]
	public ColourPalette palette;

	[Header("UI")]
	public List<Image> UIBase = new List<Image>();
	public List<Image> UILight = new List<Image>();
	public List<Image> UITrim = new List<Image>();
	public List<Image> HP = new List<Image>();
	public List<Image> HPBg = new List<Image>();
	
	[Header("Text")]
	public List<TMPro.TMP_Text> TextPrimary = new List<TMPro.TMP_Text>();
	public List<TMPro.TMP_Text> TextSecondary = new List<TMPro.TMP_Text>();

	private void OnValidate()
	{
		foreach (Image image in UIBase) { image.color = palette.UIBase; }
		foreach (Image image in UILight) { image.color = palette.UILight; }
		foreach (Image image in UITrim) { image.color = palette.UITrim; }
		foreach (Image image in HP) { image.color = palette.HP; }
		foreach (Image image in HPBg) { image.color = palette.HPBg; }

		foreach (TMPro.TMP_Text text in TextPrimary) { text.color = palette.TextPrimary; }
		foreach (TMPro.TMP_Text text in TextSecondary) { text.color = palette.TextSecondary; }
	}
}
