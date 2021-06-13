using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Settings Injecter")]
public class SettingsInjecter : ScriptableObject
{
	public GameSettings GameSettings;
	public MapSettings MapSettings;
}
