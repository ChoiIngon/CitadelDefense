using UnityEngine;
using System.Collections;

public class AttackBuff {
	public enum AttackBuffType {
		None,
		Fire
	}

	public AttackBuffType type;
	public float time;
	public float interval;
	public float value;
}

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
public class AttackBox : MonoBehaviour {
	public enum AttackElementType
	{
		None,
		Fire,
		Ice,
		Electricity,
		Max
	}
		
	public int damage;

	public AttackElementType attackType = AttackElementType.None;
	// Use this for initialization
	protected void Start () {
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D> ();
		rigidbody.isKinematic = true;

		BoxCollider2D boxCollider = GetComponent<BoxCollider2D> ();
		boxCollider.isTrigger = true;
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if ("Enemy" == col.gameObject.tag) {
			Enemy enemy = col.gameObject.GetComponent<Enemy> ();
			enemy.Damage (damage);
		} else if("Citadel" == col.gameObject.tag) {
		}
	}
}
