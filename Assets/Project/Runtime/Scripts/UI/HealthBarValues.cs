using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarValues : MonoBehaviour
{
	[Header("References")]
	public TMPro.TMP_Text CurrentHealthText;
	public TMPro.TMP_Text MaxHealthText;

	[Header("Settings")]
	public float CurrentHealth;
	public float MaxHealth;

	private void Update()
	{
		if (CurrentHealth / MaxHealth >= 0.9)
		{
			MaxHealthText.enabled = false;
		}
		else
		{
			MaxHealthText.enabled = true;
		}
		SetCurrentHealth(CurrentHealth);
		SetMaxHealth(MaxHealth);
	}

	private void SetMaxHealth(float value)
	{
		MaxHealthText.text = "/ " + value.ToString();
	}

	private void SetCurrentHealth(float value)
	{
		CurrentHealthText.text = value.ToString();
	}
}
