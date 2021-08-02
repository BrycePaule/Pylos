using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadButton : MonoBehaviour
{
	public void LoadMainMenu() => SceneLoader.Instance.LoadScene(Scenes.MainMenu);
	public void LoadGame() => SceneLoader.Instance.LoadScene(Scenes.InGame);
}
