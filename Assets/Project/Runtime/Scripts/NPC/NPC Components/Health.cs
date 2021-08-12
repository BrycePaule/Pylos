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
		if (CurrentHealth == npcBase.NPCStatAsset.MaxHealth)
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
		CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, npcBase.NPCStatAsset.MaxHealth);
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
		slider.value = CurrentHealth / npcBase.NPCStatAsset.MaxHealth;
	}

	public void Kill() 
	{
		ProcessDrops();
		Instantiate(OnDeathSplatter.gameObject, transform.position, Quaternion.identity);
		Destroy(transform.parent.gameObject);
	}

	public void ResetHealth()
	{
		CurrentHealth = npcBase.NPCStatAsset.MaxHealth;
		slider.value = CurrentHealth;
	}

	public void ProcessDrops()
	{
		// Check if has a drop table, if not, skip
		if (npcBase.NPCStatAsset.DropTable == null)
		{
			return;
		}

		Vector2Int currentLocation = npcMovement.TileLoc;
		List<int> itemDrops = npcBase.NPCStatAsset.DropTable.Roll();

		foreach (int id in itemDrops)
		{
			GameObject itemObject = ItemTable.Instance.BuildItemGameObject(id);
			itemObject.transform.position = TileConversion.TileToWorld3D(currentLocation);

			MapBoard.Instance.GetTile(currentLocation).ContainedObjects.Add(itemObject);
		}
	}

}
