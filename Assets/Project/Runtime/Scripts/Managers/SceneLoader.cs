using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	[Header("References")]
	public GameObject LoadingScreen;

	private static SceneLoader _instance;

	List<AsyncOperation> scenesLoading;

	public static SceneLoader Instance
	{
		get
		{
			if (!_instance)
				_instance = FindObjectOfType<SceneLoader>();
			if (!_instance)
				_instance = new SceneLoader();
			return _instance;
		}
	}

	private void Awake() 
	{
		LoadingScreen.SetActive(false);
		SceneManager.LoadSceneAsync((int) Scenes.MainMenu, LoadSceneMode.Additive);
	}

	public void LoadScene(Scenes scene)
	{ 
		scenesLoading = new List<AsyncOperation>();
		scenesLoading.Add(SceneManager.LoadSceneAsync((int) scene, LoadSceneMode.Additive));
	}

	public IEnumerator GetSceneLoadProgress()
	{
		for (int i = 0; i < scenesLoading.Count; i++)
		{
			while (!scenesLoading[i].isDone)
			{
				yield return null;
			}
		}

		LoadingScreen.SetActive(false);
	}
}
