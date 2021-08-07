using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ShrubBuilder : MonoBehaviour
{
	[Header("References")]
	public GameObject ClutterPrefab;
	public List<Sprite> Shrubs;

	[Header("Settings")]
	public int TextureSize;

	public Texture2D texture;

	private SpriteRenderer sr;

	private void Awake()
	{
		ResetTexture();
		sr = GetComponent<SpriteRenderer>();
	}

	public void ResetTexture()
	{
		texture = new Texture2D(TextureSize, TextureSize);
		ApplyTexture();
	}

	public void AddObject() 
	{
		int rand = Random.Range(0, Shrubs.Count - 1);
		var pixels = Shrubs[rand].texture.GetPixels(
			(int) Shrubs[rand].textureRect.x, 
			(int) Shrubs[rand].textureRect.y, 
			(int) Shrubs[rand].textureRect.height, 
			(int) Shrubs[rand].textureRect.width);

		texture.SetPixels(pixels);
		texture.Apply();
	}

	public void ApplyTexture()
	{
		sr.material.mainTexture = texture;
	}
}
