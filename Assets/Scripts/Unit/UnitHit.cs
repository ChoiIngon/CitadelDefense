using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
[RequireComponent(typeof(Rigidbody2D))]
public class UnitHit : MonoBehaviour {
	public enum ColliderType
	{
		SingleTarget,
		Box,
		Circle,
		Ellipse
	}
	public UnitAttack attack;
    public bool shakeCamera;
	public int hitCount = int.MaxValue;
	public ColliderType colliderType;

    public virtual void Start()
    {
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D> ();
		rigidbody.isKinematic = true;
	
		Collider2D collider = GetComponent<Collider2D> ();
		collider.isTrigger = true;

	    if (true == shakeCamera)
        {
            Hashtable ht = new Hashtable();
            ht.Add("x", 0.1f);
            ht.Add("y", 0.1f);
            ht.Add("time", 0.3f);
            iTween.ShakePosition(Camera.main.gameObject, ht);
        }
    }
		
    void OnTriggerEnter2D(Collider2D col)
    {
        if (attack.targetTag == col.gameObject.tag && 0 < hitCount)
        {
            UnitColliderDamage colliderDamage = col.gameObject.GetComponent<UnitColliderDamage>();
            if (null != colliderDamage)
            {
                hitCount -= 1;
                attack.Damage(colliderDamage.unit);
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(UnitHit))]
public class UnitHitEditor : Editor
{

	void OnEnable()
	{
	}
	public override void OnInspectorGUI()
	{
		UnitHit hit = (UnitHit)target;
		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.LabelField ("Shake Camera");
		hit.shakeCamera = EditorGUILayout.Toggle (hit.shakeCamera);
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.LabelField ("Hit Count");
		hit.hitCount = EditorGUILayout.IntField (hit.hitCount);
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.LabelField ("Collider Type");
		UnitHit.ColliderType newColliderType = (UnitHit.ColliderType)EditorGUILayout.EnumPopup(hit.colliderType);
		EditorGUILayout.EndHorizontal ();

		if (newColliderType != hit.colliderType) {
			switch (hit.colliderType) {
			case UnitHit.ColliderType.Box:
				DestroyImmediate(hit.gameObject.GetComponent<BoxCollider2D> ());
				break;
			case UnitHit.ColliderType.Circle:
				DestroyImmediate(hit.gameObject.GetComponent<CircleCollider2D> ());
				break;
			case UnitHit.ColliderType.Ellipse:
				DestroyImmediate (hit.gameObject.GetComponent<EllipseCollider2D> ());
				DestroyImmediate (hit.gameObject.GetComponent<EdgeCollider2D> ());
				break;
			}

			switch (newColliderType) {
			case UnitHit.ColliderType.Box:
				hit.gameObject.AddComponent<BoxCollider2D> ().isTrigger = true;;
				break;
			case UnitHit.ColliderType.Circle:
				hit.gameObject.AddComponent<CircleCollider2D> ().isTrigger = true;
				break;
			case UnitHit.ColliderType.Ellipse:
				hit.gameObject.AddComponent<EllipseCollider2D> ();
				hit.gameObject.GetComponent<EdgeCollider2D> ().isTrigger = true;;
				break;
			}
		}

		hit.colliderType = newColliderType;
	}
}
#endif