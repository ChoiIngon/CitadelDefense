using UnityEngine;
using System.Collections;

public class UnitAttack_Bullet : UnitAttack {
	public override void Attack()
	{
		Effect go = GameObject.Instantiate<Effect> (info.effect);
		go.transform.position = target.transform.position;
		target.Damage (info.power);
	}
}
