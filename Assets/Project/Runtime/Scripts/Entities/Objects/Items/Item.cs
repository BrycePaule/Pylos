using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Data Packs/Items/Item")]
public class Item : ScriptableObject
{
	public int ID;
	public string Name;

	public Sprite Sprite;
	public Sprite Icon;

	public Recipe Recipe;
}
