using UnityEngine;
using System.Collections;

public class UnitAttack_PoisionBolt : UnitAttack_Missile {

	public override void Damage(Unit unit)
	{
		unit.Damage ((int)data.power);
		if (null != buffPrefab) {
			Buff_Poison buff = (Buff_Poison)GameObject.Instantiate(buffPrefab);
			buff.unit = unit;
			buff.damage = data.power;
			buff.transform.SetParent (unit.transform);
		}
	}
}
