using UnityEngine;
using System.Collections;

public class CitadelBuff_AttackPower : CitadelBuff {
	public override void Init()
	{
		foreach (HeroUnit unit in GameManager.Instance.heros) {
			if (null != unit.passiveAttack) {
				unit.passiveAttack.powerBuff -= this.Buff;
				unit.passiveAttack.powerBuff += this.Buff;
			}
		}
	}
	public override void Upgrade()
	{
		
	}
}
