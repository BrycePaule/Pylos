using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable<float>
{

	[SerializeField] public float MaxHealth;
	[SerializeField] public float CurrentHealth;

	private void Awake() 
	{
		CurrentHealth = MaxHealth;
	}

	public void Damage(float damage)
	{
		CurrentHealth -= damage;
		if (CurrentHealth <= 0) { Kill(); }
	}

	public void Kill() => Destroy(transform.parent.gameObject);

}
