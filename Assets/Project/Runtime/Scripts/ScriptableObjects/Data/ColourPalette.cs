using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data Packs/Colour Palette")]
public class ColourPalette : ScriptableObject
{
	[Header("UI")]
	public Color UIBase;
	public Color UILight;
	public Color UITrim;

	public Color HP;
	public Color HPBg;
	
	[Header("Text")]
	public Color TextPrimary;
	public Color TextSecondary;

	[Header("Tiles")]
	public Color Water;
	public Color Sand;
	public Color Dirt;
	public Color Grass;
	public Color Stone;
}
