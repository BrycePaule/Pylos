using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Data Packs/Items/Item")]
public class ItemAsset : ScriptableObject
{
	public int ID;
	public string Name;

	public GameObject Prefab;
	public Sprite Sprite;
	public Sprite Icon;

	public Recipe Recipe;
}
