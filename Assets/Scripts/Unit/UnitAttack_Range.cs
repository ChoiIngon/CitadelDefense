using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAttack_Range : UnitAttack
{
    public Missile missilePrefab;
    public override void Attack()
    {
		if (null == target) {
			return;
		}
		Missile missile = Object.Instantiate<Missile> (missilePrefab);
		missile.attack = this;
		missile.Init (transform.position, target.transform.position, data.power);
    }
}
