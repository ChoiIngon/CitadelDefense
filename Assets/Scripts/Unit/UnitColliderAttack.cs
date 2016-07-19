using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class UnitColliderAttack : MonoBehaviour {
	public UnitAttack attack;
    public int hitCount = int.MaxValue;
    // Use this for initialization
	protected void Start () {
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D> ();
		rigidbody.isKinematic = true;

		Collider2D collider = GetComponent<Collider2D> ();
		collider.isTrigger = true;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (attack.targetTag == col.gameObject.tag && 0 < hitCount) {
			UnitColliderDamage colliderDamage = col.gameObject.GetComponent<UnitColliderDamage> ();
			if (null != colliderDamage) {
                hitCount -= 1;
				attack.Damage (colliderDamage.unit);
			}
		}
	}
}
