using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAttack_Missile : UnitAttack
{
	public int missileCount = 1;
	public Missile missilePrefab;
    public override void Attack()
    {
		if (null == target) {
			return;
		}
		for (int i = 0; i < missileCount; i++) {
			Missile missile = Object.Instantiate<Missile> (missilePrefab);
			missile.Init (transform.position, target.hitPoint, this, self.altitude);
		}
    }
}
