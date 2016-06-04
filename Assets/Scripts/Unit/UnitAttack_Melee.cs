using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class UnitAttack_Melee : UnitAttack {
	Unit unit;
	public override void Attack()
	{
		
	}

	protected void Start () {
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D> ();
		rigidbody.isKinematic = true;

		BoxCollider2D boxCollider = GetComponent<BoxCollider2D> ();
		boxCollider.isTrigger = true;
	}

	void OnTriggerEnter2D(Collider2D col) {
		unit = col.gameObject.GetComponent<Unit> ();
	}
}
