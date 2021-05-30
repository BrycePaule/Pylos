using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamageable<float>
{

	[SerializeField] public float MaxHealth;
	[SerializeField] public float CurrentHealth;
	[SerializeField] private Canvas HealthBarCanvas;
	[SerializeField] private GameObject HealthBar;

	private Slider slider;
	private NPCMovement npcMovement;

	private void Awake() 
	{
		slider = HealthBar.GetComponentInChildren<Slider>();
		npcMovement = transform.parent.GetComponentInChildren<NPCMovement>();
	}

	private void Start() 
	{
		CurrentHealth = MaxHealth;
		slider.value = CurrentHealth;
	}

	private void Update()
	{
		if (CurrentHealth == MaxHealth)
		{
			DisableHealthBar();
		}
		else
		{
			EnableHealthBar();
			UpdateHealthBarSlider();
		}
	}

	public void Damage(float value, GameObject damagedBy)
	{
		CurrentHealth -= value;
		UpdateHealthBarSlider();
		if (CurrentHealth <= 0) { Kill(); }

		if (!npcMovement.Aggro && damagedBy != null) { npcMovement.AggroOnTo(damagedBy); }
	}
	
	public void Heal(float value)
	{
		CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, MaxHealth);
		UpdateHealthBarSlider();
	}

	private void EnableHealthBar()
	{
		HealthBarCanvas.gameObject.SetActive(true);
	}

	private void DisableHealthBar()
	{
		HealthBarCanvas.gameObject.SetActive(false);
	}

	private void UpdateHealthBarSlider()
	{
		slider.value = CurrentHealth / MaxHealth;
	}

	public void Kill() => Destroy(transform.parent.gameObject);

}
