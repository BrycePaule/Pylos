using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// [CustomPropertyDrawer(typeof(ItemCount))]
public class ItemCountDrawerOld : PropertyDrawer
{
	public int lines = 1;
	public int lineHeight = 20;
	public int lineSpace = 1;
	public int wordSpace = 5;

	Rect pos;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
	{
		EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		pos = position;
		var itemIDRect = new Rect(position.x, position.y, PropWidth(.3f), lineHeight);
		var countRect = new Rect(position.x + position.width * 0.3f, position.y, PropWidth(0.7f), lineHeight);

		EditorGUI.PropertyField(itemIDRect, property.FindPropertyRelative("ID"), GUIContent.none);
		EditorGUI.PropertyField(countRect, property.FindPropertyRelative("Count"), GUIContent.none);

		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return base.GetPropertyHeight(property, label) * lines + (lines * lineSpace * 2);
	}

	public float PropWidth(float percentage)
	{
		return pos.width * percentage - wordSpace;
	}

}