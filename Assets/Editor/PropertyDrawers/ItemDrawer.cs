using UnityEngine;
using UnityEditor;

// [CustomPropertyDrawer(typeof(Item))]
public class ItemDrawer : PropertyDrawer 
{
	// int fieldWidth = 40;
	// int labelWidth = 40;
	int lineSpace = 3;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
	{
		base.OnGUI(position, property, label);

		// EditorGUI.BeginProperty(position, label, property);

		// 	// Debug.Log(property.FindPropertyRelative("ID").intValue.ToString());

		// 	// position = EditorGUI.PrefixLabel(position, new GUIContent(Resources.Load<ItemTable>("Tables/Item Table").Lookup(property.FindPropertyRelative("ID").intValue).ToString()));

		// 	int indent = EditorGUI.indentLevel;
		// 	EditorGUI.indentLevel = 0;

		// 	var IDLabelRect = new Rect(position.x, position.y, labelWidth, position.height - lineSpace);
		// 	var IDRect = new Rect(position.x + position.width * 0.1f, position.y, fieldWidth, position.height - lineSpace);
		// 	var NameLabelRect = new Rect(position.xMax - position.width * 0.4f, position.y, labelWidth, position.height - lineSpace);
		// 	var NameRect = new Rect(position.xMax - position.width * 0.2f, position.y, fieldWidth, position.height - lineSpace);

		// 	EditorGUI.LabelField(IDLabelRect, "ID");
		// 	EditorGUI.PropertyField(IDRect, property.FindPropertyRelative("ID"), GUIContent.none);
		// 	EditorGUI.LabelField(NameLabelRect, "Name");
		// 	EditorGUI.PropertyField(NameRect, property.FindPropertyRelative("Name"), GUIContent.none);

		// 	EditorGUI.indentLevel = indent;

		// EditorGUI.EndProperty();
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return base.GetPropertyHeight(property, label) + lineSpace;
	}
}
