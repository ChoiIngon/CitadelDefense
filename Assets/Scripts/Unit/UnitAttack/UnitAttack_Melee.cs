using UnityEngine;
using System.Collections;

public class UnitAttack_Melee : UnitAttack {
	public override void Attack()
	{
		if (null == hitPrefab) {
			if (null != target) {
				Damage (target);
			}
		} else {
			Hit (transform.position);
		}
	}
}
