using UnityEngine;
using System.Collections;


[RequireComponent(typeof(UnitColliderAttack))]
public class UnitAttack_Melee : UnitAttack {
    private Unit targetUnit;
	private UnitColliderAttack unitColliderAttack;
	public override void Attack()
	{
        if (null != targetUnit)
        {
            targetUnit.Damage(power);
        }
	}

	protected void Start () {
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D> ();
		rigidbody.isKinematic = true;

		BoxCollider2D boxCollider = GetComponent<BoxCollider2D> ();
		boxCollider.isTrigger = true;

		unitColliderAttack = GetComponent<UnitColliderAttack> ();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (unitColliderAttack.targetUnitTag == col.gameObject.tag)
        {
            UnitColliderDamage colliderDamage = col.gameObject.GetComponent<UnitColliderDamage>();
            if (null != colliderDamage)
            {
                targetUnit = colliderDamage.unit;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (unitColliderAttack.targetUnitTag == col.gameObject.tag)
        {
            targetUnit = null;
        }
    }
}
