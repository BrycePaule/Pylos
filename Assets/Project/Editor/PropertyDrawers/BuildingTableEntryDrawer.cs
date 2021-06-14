using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(BuildingTableEntry))]
public class BuildingTableEntryDrawer: PropertyDrawer 
{
	public int lines = 3;
	public int lineHeight = 20;
	public int lineSpace = 1;
	public int wordSpace = 5;

	public int idWidth = 100;
	public int nameWidth = 100;
	public int spriteWidth = 100;
	public int iconWidth = 100;

	Rect pos;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
	{
		EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		pos = position;
		var idRect = new Rect(position.x, position.y, PropWidth(.3f), lineHeight);
		var nameRect = new Rect(position.x + position.width * 0.3f, position.y, PropWidth(0.7f), lineHeight);
		var spriteRect = new Rect(position.x, position.y + (lineHeight * 1) + (lineSpace * 1), position.width, lineHeight);
		var iconRect = new Rect(position.x, position.y + (lineHeight * 2) + (lineSpace * 2), position.width, lineHeight);

		EditorGUI.PropertyField(idRect, property.FindPropertyRelative("ID"), GUIContent.none);
		EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("Name"), GUIContent.none);
		EditorGUI.PropertyField(spriteRect, property.FindPropertyRelative("Sprite"), new GUIContent("Sprite"));
		EditorGUI.PropertyField(iconRect, property.FindPropertyRelative("Icon"), new GUIContent("Icon"));


		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return base.GetPropertyHeight(property, label) * lines + (lines * lineSpace * 2) + 5;
	}

	public float PropWidth(float percentage)
	{
		return pos.width * percentage - wordSpace;
	}

}
