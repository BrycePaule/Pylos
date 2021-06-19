using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Colors
{
	// Randomises color based on hue/sat/vib % differences
	public static Color RandomiseColour(Color colour, float hueChange = 0, float satChange = 0, float vibChange = 0)
	{
		float h, s, v;
		Color.RGBToHSV(colour, out h, out s, out v);
		return Color.HSVToRGB(
			h + Random.Range(h * -hueChange, h * hueChange), 
			s + Random.Range(s * -satChange, s * satChange),
			v + Random.Range(v * -vibChange, v * vibChange)
		);
	}

	public static Color AlterColour(Color colour, bool invert = false, float hueChange = 0, float satChange = 0, float vibChange = 0)
	{
		float h, s, v;
		Color.RGBToHSV(colour, out h, out s, out v);

		int inversion = invert ? -1 : 1;

		float Hdrandom = h * Random.Range(-0.02f, 0.02f) * inversion;
		float Sdrandom = s * Random.Range(-0.02f, 0.02f) * inversion;
		float Vdrandom = v * Random.Range(-0.02f, 0.02f) * inversion;

		return Color.HSVToRGB(h + Hdrandom, s + Sdrandom, v + Vdrandom);
	}

	public static Color Darken(Color colour, float percent = 0.05f)
	{
		float h, s, v;
		Color.RGBToHSV(colour, out h, out s, out v);

		v -= v * percent;
		s -= s * percent;

		return Color.HSVToRGB(h, s, v);
	}
}
