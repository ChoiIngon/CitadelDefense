using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AttackBox))]
public class UnitAttack_Melee : UnitAttack {
	public GameObject effect;
	public float direction;

	public override void Attack()
	{
	}
}
