using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampedInt : MonoBehaviour
{
	private int value;

	private int min;
	private int max;

	public ClampedInt(int _value, int _min = 0, int _max = 100)
	{
		value = _value;
		value = _min;
		value = _max;
	}

	public static ClampedInt operator +(ClampedInt a) => a;
	public static ClampedInt operator -(ClampedInt a) => new ClampedInt(-a.value);

	public static ClampedInt operator +(ClampedInt a, ClampedInt b)
	{
		if (a.value + b.value >= a.max)
			return new ClampedInt(a.max, a.min, a.max);
		else
			return new ClampedInt(a.value + b.value, a.min, a.max);
	}

	public static ClampedInt operator -(ClampedInt a, ClampedInt b)
	{
		if (a.value - b.value <= a.min)
			return new ClampedInt(a.min, a.min, a.max);
		else
			return new ClampedInt(a.value - b.value, a.min, a.max);
	}

	public static ClampedInt operator *(ClampedInt a, ClampedInt b)
	{
		if (a.value * b.value <= a.min)
			return new ClampedInt(a.min, a.min, a.max);
		if (a.value * b.value >= a.max)
			return new ClampedInt(a.max, a.min, a.max);
		else
			return new ClampedInt(a.value * b.value, a.min, a.max);
	}

	public static ClampedInt operator /(ClampedInt a, ClampedInt b)
	{
		if (a.value / b.value <= a.min)
			return new ClampedInt(a.min, a.min, a.max);
		if (a.value / b.value >= a.max)
			return new ClampedInt(a.max, a.min, a.max);
		else
			return new ClampedInt(a.value / b.value, a.min, a.max);
	}
	
	public static ClampedInt operator %(ClampedInt a, ClampedInt b)
	{
		if (a.value % b.value <= a.min)
			return new ClampedInt(a.min, a.min, a.max);
		if (a.value % b.value >= a.max)
			return new ClampedInt(a.max, a.min, a.max);
		else
			return new ClampedInt(a.value % b.value, a.min, a.max);
	}

	public override string ToString()
	{
		return base.ToString() + ": " + value.ToString();
	}
}
