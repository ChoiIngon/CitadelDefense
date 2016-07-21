using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

//[RequireComponent(typeof(SpriteRenderer))]
public class Unit : MonoBehaviour {
	public enum Elemental
	{
		None,
		Fire,
		Ice,
		Electricity,
		Max
	}
	public enum UnitSize
	{
		Small,
		Middle,
		Large
	}
	public UnitSize size;
	public Elemental elemental;
	public UnitMove unitMove;
	public UnitAnimation unitAnimation;
    public Dictionary<string, Buff> buffs = new Dictionary<string, Buff>();

	public virtual void Start() {
		float scale = 1.0f;
		switch (size) {
		case UnitSize.Small:
			scale = 0.3f;
			break;
		case UnitSize.Middle:
			scale = 0.5f;
			break;
		case UnitSize.Large:
			scale = 1.0f;
			break;
		}
		//Scale (transform, scale);

		transform.localScale *= scale;
	}
    
	public virtual void Damage(int damage) {}

	/*
	private void Scale(Transform transform, float scale)
	{
		transform.localScale = transform.localScale * scale;
		foreach (Transform child in transform)
		{
			Scale (child, scale);
		}        
	}
	*/
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