using UnityEngine;
using System.Collections;


[RequireComponent(typeof(UnitColliderAttack))]
public class UnitAttack_Melee : UnitAttack {
	private UnitColliderAttack unitColliderAttack;
	public override void Attack()
	{
		unitColliderAttack.enabled = true;
	}

	protected void Start () {
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D> ();
		rigidbody.isKinematic = true;

		BoxCollider2D boxCollider = GetComponent<BoxCollider2D> ();
		boxCollider.isTrigger = true;

		unitColliderAttack = GetComponent<UnitColliderAttack> ();
		unitColliderAttack.enabled = false;
	}

	void OnTriggerEnter2D(Collider2D col) {
		Unit unit = col.gameObject.GetComponent<Unit> ();
		//unit.Damage (unitColliderAttack.attackPower);
	}
}
