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
	public Rect attackBoxSize;
	public AttackElementType attackType = AttackElementType.None;
	protected BoxCollider2D attackBox;
	// Use this for initialization
	protected void Start () {
		attackBox = GetComponent<BoxCollider2D> ();
		attackBox.size = new Vector2(attackBoxSize.width, attackBoxSize.height);
		attackBox.offset = new Vector2(attackBoxSize.x, attackBoxSize.y);
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if ("Enemy" == col.gameObject.tag) {
			Enemy enemy = col.gameObject.GetComponent<Enemy> ();
			enemy.Damage (damage);
		} else if("Citadel" == col.gameObject.tag) {
		}
	}
}
