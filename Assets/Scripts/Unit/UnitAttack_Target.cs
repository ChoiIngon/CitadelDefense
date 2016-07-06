using UnityEngine;
using System.Collections;

public class UnitAttack_Target : UnitAttack {
    public Effect effectPrefab;
	public override void Attack()
	{
		if (null == target) {
			return;
		}
	
		Effect go = GameObject.Instantiate<Effect> (effectPrefab);
		go.transform.position = target.transform.position;
		target.Damage ((int)data.power);
	}
}
