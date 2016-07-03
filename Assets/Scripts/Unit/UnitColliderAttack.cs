using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class UnitColliderAttack : MonoBehaviour {
	public string targetUnitTag;
	public float power;
	// Use this for initialization
	protected void Start () {
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D> ();
		rigidbody.isKinematic = true;

		Collider2D collider = GetComponent<Collider2D> ();
		collider.isTrigger = true;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (targetUnitTag == col.gameObject.tag) {
			UnitColliderDamage colliderDamage = col.gameObject.GetComponent<UnitColliderDamage> ();
			if (null != colliderDamage) {
				colliderDamage.unit.Damage ((int)power);
			}
		}
	}
}
