using UnityEngine;
using System.Collections;

public class CitadelBuff_AttackPower : CitadelBuff {
	public override void Init()
	{
		foreach (var itr in GameManager.Instance.citadel.heros) {
			HeroUnit unit = itr.Value;
			if (null != unit.passiveAttack) {
				unit.passiveAttack.powerBuff -= this.Buff;
				unit.passiveAttack.powerBuff += this.Buff;
			}
		}
	}
}
