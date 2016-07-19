using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

//[RequireComponent(typeof(SpriteRenderer))]
public class Unit : MonoBehaviour {
	public UnitMove unitMove;
	public UnitAnimation unitAnimation;
    public Dictionary<string, Buff> buffs = new Dictionary<string, Buff>();

    public virtual void Init() {}
	public virtual void Damage(int damage) {}
}

public class ReadOnlyAttribute : PropertyAttribute
{
}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
	public override float GetPropertyHeight(SerializedProperty property,
		GUIContent label)
	{
		return EditorGUI.GetPropertyHeight(property, label, true);
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		GUI.enabled = false;
		EditorGUI.PropertyField(position, property, label, true);
		GUI.enabled = true;
	}
}
#endif