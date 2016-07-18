using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class UnitColliderAttack : MonoBehaviour {
	public UnitAttack attack;
    // Use this for initialization
	protected void Start () {
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D> ();
		rigidbody.isKinematic = true;

		Collider2D collider = GetComponent<Collider2D> ();
		collider.isTrigger = true;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (attack.targetTag == col.gameObject.tag) {
			UnitColliderDamage colliderDamage = col.gameObject.GetComponent<UnitColliderDamage> ();
			if (null != colliderDamage) {
				attack.Damage (colliderDamage.unit);
			}
		}
	}
}
