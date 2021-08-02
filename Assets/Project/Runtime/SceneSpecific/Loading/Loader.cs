using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
	private static Action onLoaderCallback;

	public static void LoadScene(int index)
	{
		foreach (Scenes scene in System.Enum.GetValues(typeof(Scenes)))
		{
			if (scene.GetHashCode() == index)
			{
				onLoaderCallback = () => {
					SceneManager.LoadScene((int) scene);
				};
			}
		}

		SceneManager.LoadScene((int) Scenes.Loading);
	}

	public static void UpdateCallback()
	{
		if (onLoaderCallback != null)
		{
			onLoaderCallback();
			onLoaderCallback = null;
		}
	}
}
