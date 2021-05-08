using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadius : MonoBehaviour
{
	[SerializeField] int attackRadius;
	[SerializeField] int damage;

	private Faction faction;

	private void Awake()
	{
		faction = GetComponentInParent<MobBase>().Faction;
		GetComponent<CircleCollider2D>().radius = attackRadius;
	}

	private void OnTriggerEnter2D(Collider2D other) 
	{
		GameObject otherObject = other.transform.parent.gameObject;

		if (otherObject.layer != Layer.Mobs.GetHashCode()) { return; }
		if (otherObject.GetComponentInChildren<MobBase>().Faction == faction) { return; }

		IDamageable<float> target = otherObject.GetComponentInChildren<IDamageable<float>>();
		if (target != null)
		{
			target.Damage(damage);
			// print(transform.parent.name + " attacking " + other.transform.parent.name);
		}
	}
}
