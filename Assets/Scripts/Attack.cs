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

[RequireComponent (typeof (SpriteRenderer))]
[RequireComponent (typeof (BoxCollider2D))]

public class Attack : MonoBehaviour {
	public enum AttackElementType
	{
		None,
		Fire,
		Ice,
		Electricity,
		Max
	}
		
	public int damage;
	public Rect size;
	public AttackElementType attackType = AttackElementType.None;
	private BoxCollider2D attackBox;
	// Use this for initialization
	protected void Start () {
		attackBox = GetComponent<BoxCollider2D> ();
		attackBox.size = new Vector2(size.width, size.height);
		attackBox.offset = new Vector2(size.x, size.y);
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log ("crash");
		if ("Enemy" == col.gameObject.tag) {
			Enemy enemy = col.gameObject.GetComponent<Enemy> ();
			enemy.Damage (damage);
		} else if("Citadel" == col.gameObject.tag) {
		}
	}
}
