using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : NPCComponentBase, IDamageable<float>
{
	[Header("References")]
	public ParticleSystem OnHitSplatter;
	public ParticleSystem OnDeathSplatter;

	[Header("Settings")]
	public float MaxHealth;
	public float CurrentHealth;
	public Canvas HealthBarCanvas;
	public GameObject HealthBar;

	private Slider slider;

	private Movement npcMovement;
	private Aggro npcAggro;

	protected override void Awake() 
	{
		base.Awake();
		npcBase.SubscribeComponent(NPCComponentType.Health, this);

		slider = HealthBar.GetComponentInChildren<Slider>();
	}
	
	private void Start()
	{
		npcMovement = (Movement) npcBase.GetNPCComponent(NPCComponentType.Movement);
		npcAggro = (Aggro) npcBase.GetNPCComponent(NPCComponentType.Aggro);
		ResetHealth();
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

	public void Damage(float value, NPCBase damagedBy)
	{
		CurrentHealth -= value;
		UpdateHealthBarSlider();

		if (CurrentHealth <= 0) { Kill(); }

		npcAggro.Increment(damagedBy); 
		Instantiate(OnHitSplatter.gameObject, transform.position, Quaternion.identity);
	}

	public void Heal(float value, GameObject healedBy)
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

	public void Kill() 
	{
		Instantiate(OnDeathSplatter.gameObject, transform.position, Quaternion.identity);
		Destroy(transform.parent.gameObject);
	}

	public void ResetHealth()
	{
		CurrentHealth = MaxHealth;
		slider.value = CurrentHealth;
	}

}
