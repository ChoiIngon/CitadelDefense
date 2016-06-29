using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
public class UnitColliderAttack : MonoBehaviour {
	public string targetUnitTag;
	public float attackPower;
	// Use this for initialization
	protected void Start () {
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D> ();
		rigidbody.isKinematic = true;

		BoxCollider2D boxCollider = GetComponent<BoxCollider2D> ();
		boxCollider.isTrigger = true;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (targetUnitTag == col.gameObject.tag) {
			UnitColliderDamage colliderDamage = col.gameObject.GetComponent<UnitColliderDamage> ();
			if (null != colliderDamage) {
				colliderDamage.unit.Damage ((int)attackPower);
			}
		}
	}
}
