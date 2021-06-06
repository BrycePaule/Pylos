using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : NPCComponentBase, IDamageable<float>
{
	[SerializeField] public float MaxHealth;
	[SerializeField] public float CurrentHealth;
	[SerializeField] private Canvas HealthBarCanvas;
	[SerializeField] private GameObject HealthBar;

	public UnityEvent OnDeathEvent;

	private Slider slider;

	public Movement npcMovement;
	public Aggro npcAggro;

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

	public void Kill() 
	{
		// OnDeathEvent.Invoke();
		Destroy(transform.parent.gameObject);
	}

	public void ResetHealth()
	{
		CurrentHealth = MaxHealth;
		slider.value = CurrentHealth;
	}

}
