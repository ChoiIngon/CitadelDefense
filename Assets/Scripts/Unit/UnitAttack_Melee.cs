using UnityEngine;
using System.Collections;

public class UnitAttack_Melee : UnitAttack {
	//public string targetTag;
	public override void Attack()
	{
        if (null != target)
        {
            Damage(target);
        }
	}
}
