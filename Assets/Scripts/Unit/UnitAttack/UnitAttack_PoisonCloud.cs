using UnityEngine;
using System.Collections;

public class UnitAttack_PoisonCloud : UnitAttack {
	public override void Attack()
	{
		Hit (new Vector3(8.2f, 0.8f, 0.0f));
    }
}
