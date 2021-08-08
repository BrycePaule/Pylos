using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampedFloat
{
	private float value;

	private float min;
	private float max;

	public ClampedFloat(float _value, float _min = 0, float _max = 100)
	{
		value = _value;
		value = _min;
		value = _max;
	}

	public static ClampedFloat operator +(ClampedFloat a) => a;
	public static ClampedFloat operator -(ClampedFloat a) => new ClampedFloat(-a.value);

	public static ClampedFloat operator +(ClampedFloat a, ClampedFloat b)
	{
		if (a.value + b.value >= a.max)
			return new ClampedFloat(a.max, a.min, a.max);
		else
			return new ClampedFloat(a.value + b.value, a.min, a.max);
	}

	public static ClampedFloat operator -(ClampedFloat a, ClampedFloat b)
	{
		if (a.value - b.value <= a.min)
			return new ClampedFloat(a.min, a.min, a.max);
		else
			return new ClampedFloat(a.value - b.value, a.min, a.max);
	}

	public static ClampedFloat operator *(ClampedFloat a, ClampedFloat b)
	{
		if (a.value * b.value <= a.min)
			return new ClampedFloat(a.min, a.min, a.max);
		if (a.value * b.value >= a.max)
			return new ClampedFloat(a.max, a.min, a.max);
		else
			return new ClampedFloat(a.value * b.value, a.min, a.max);
	}

	public static ClampedFloat operator /(ClampedFloat a, ClampedFloat b)
	{
		if (a.value / b.value <= a.min)
			return new ClampedFloat(a.min, a.min, a.max);
		if (a.value / b.value >= a.max)
			return new ClampedFloat(a.max, a.min, a.max);
		else
			return new ClampedFloat(a.value / b.value, a.min, a.max);
	}
	
	public static ClampedFloat operator %(ClampedFloat a, ClampedFloat b)
	{
		if (a.value % b.value <= a.min)
			return new ClampedFloat(a.min, a.min, a.max);
		if (a.value % b.value >= a.max)
			return new ClampedFloat(a.max, a.min, a.max);
		else
			return new ClampedFloat(a.value % b.value, a.min, a.max);
	}

	public override string ToString()
	{
		return base.ToString() + ": " + value.ToString();
	}
}
