using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRadius : MonoBehaviour
{
	[SerializeField] int detectionRadius;

	private Faction faction;

	private void Awake()
	{
		faction = GetComponentInParent<NPCBase>().Faction;
		GetComponent<CircleCollider2D>().radius = detectionRadius;
	}

	private void OnTriggerEnter2D(Collider2D other) 
	{
		
	}
}
