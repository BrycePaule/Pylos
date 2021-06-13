using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionRing : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;

	public void Select() => spriteRenderer.enabled = true; 
	public void Deselect() => spriteRenderer.enabled = false; 
}
