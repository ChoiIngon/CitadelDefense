using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
public class DamageBox : MonoBehaviour {
	Unit unit;
	public void Init(Unit unit)
	{
		this.unit = unit;
	}
	protected void Start () {
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D> ();
		rigidbody.isKinematic = true;

		BoxCollider2D boxCollider = GetComponent<BoxCollider2D> ();
		boxCollider.isTrigger = true;	
	}

	void OnTriggerEnter2D(Collider2D col) {
		AttackBox attack = col.gameObject.GetComponent<AttackBox> ();
		unit.Damage (attack.damage);
	}

	void OnTriggerStay2D(Collider2D col) {

	}

	void OnTriggerExit2D(Collider2D col) {

	}
}
