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
	public float altitude;
	public UnitSize size;
	public Elemental elemental;
	public UnitMove unitMove;
    public UnitAttack passiveAttack;
    public UnitAttack activeAttack;
	public UnitAnimation unitAnimation;
	public BoxCollider2D hitBox;
	public Vector3 center {
		get {
			return hitBox.bounds.center;
		}
	}
	public Vector3 hitPoint {
		get {
			if (null == hitBox) {
				return Vector3.zero;
			}
			float x = Random.Range (-hitBox.bounds.size.x/2, hitBox.bounds.size.x/2);
			float y = Random.Range (-hitBox.bounds.size.y/2, hitBox.bounds.size.y/2);
			return new Vector3 (x + hitBox.bounds.center.x, y + hitBox.bounds.center.y, 0.0f);
		}
	}
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