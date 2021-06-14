using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRadius : NPCComponentBase
{
	[Header("References")]
	public SettingsInjecter SettingsInjecter;

	[Header("Settings")]
	public int detectionRadius;

	private SpriteRenderer sr;

	private Movement npcMovement;
	private Aggro npcAggro;

	protected override void Awake() 
	{
		base.Awake();
		npcBase.SubscribeComponent(NPCComponentType.DetectionRadius, this);

		GetComponent<CircleCollider2D>().radius = detectionRadius;
		sr = GetComponent<SpriteRenderer>();
	}

	private void Start()
	{
		npcMovement = (Movement) npcBase.GetNPCComponent(NPCComponentType.Movement);
		npcAggro = (Aggro) npcBase.GetNPCComponent(NPCComponentType.Aggro);
	}

	private void FixedUpdate()
	{
		if (SettingsInjecter.GameSettings.ShowDetectionRadius)
		{
			sr.enabled = true;
		}
		else
		{
			sr.enabled = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.layer == Layer.NPC.GetHashCode() && other.GetComponent<NPCBase>().Faction != npcBase.Faction)
		{
			npcAggro = (Aggro) npcBase.GetNPCComponent(NPCComponentType.Aggro);
			npcAggro.EnterRange(other.GetComponent<NPCBase>());
		}
	}

	private void OnTriggerExit2D(Collider2D other) 
	{
		if (other.gameObject.layer == Layer.NPC.GetHashCode() && other.GetComponent<NPCBase>().Faction != npcBase.Faction)
		{
			npcAggro.ExitRange(other.GetComponent<NPCBase>());
		}
	
	}
}
