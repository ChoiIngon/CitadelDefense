using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class UnitAttack_Melee : UnitAttack {
	//public string targetTag;
	public override void Attack()
	{
        if (null != target)
        {
            Damage(target);
        }
	}

	protected void Start () {
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D> ();
		rigidbody.isKinematic = true;

		BoxCollider2D boxCollider = GetComponent<BoxCollider2D> ();
		boxCollider.isTrigger = true;
	}

	/*
    void OnTriggerEnter2D(Collider2D col)
    {
        if (targetTag == col.gameObject.tag)
        {
            UnitColliderDamage colliderDamage = col.gameObject.GetComponent<UnitColliderDamage>();
            if (null != colliderDamage)
            {
                target = colliderDamage.unit;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (targetTag == col.gameObject.tag)
        {
			if (target == col.gameObject.GetComponent<Unit> () && null != target) {
				target = null;
			}
        }
    }
    */
}
