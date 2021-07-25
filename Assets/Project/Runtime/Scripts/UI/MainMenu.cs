using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	[Header("References")]
	public SettingsInjecter SettingsInjecter;

	private void Awake() 
	{
		SettingsInjecter.GameSettings.MainMenuIsOpen = false;
	}

	public void ToggleMainMenu()
	{
		if (SettingsInjecter.GameSettings.MainMenuIsOpen)
		{
			Disable();
		}
		else
		{
			Enable();
		}
	}

	public void Enable()
	{
		gameObject.SetActive(true);
		SettingsInjecter.GameSettings.MainMenuIsOpen = true;
		Time.timeScale = 0f;
	}

	public void Disable()
	{
		gameObject.SetActive(false);
		SettingsInjecter.GameSettings.MainMenuIsOpen = false;
		Time.timeScale = 1f;
	}

	// BUTTONS

	public void QuitGame()
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}
