using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRadius : NPCComponentBase
{
	[SerializeField] int detectionRadius;

	private Faction faction;
	private NPCMovement npcMovement;

	protected override void Awake() 
	{
		base.Awake();
		npcBase.SubscribeComponent(NPCComponentType.DetectionRadius, this);

		npcMovement = transform.parent.GetComponentInChildren<NPCMovement>();
		faction = GetComponentInParent<NPCBase>().Faction;
		GetComponent<CircleCollider2D>().radius = detectionRadius;
	}

	private void OnTriggerEnter2D(Collider2D other) 
	{
		if (npcMovement.NPCMovementType == MovementType.Search) { return; }
		if (other.gameObject.layer == Layer.NPC.GetHashCode() && other.GetComponentInChildren<NPCBase>().Faction != faction)
		{
			if (!npcMovement.Aggro)
			{
				npcMovement.AggroOnTo(other.gameObject);
			}
		}
	}
}
