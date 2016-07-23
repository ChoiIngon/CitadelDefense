using UnityEngine;
using System.Collections;

public class CitadelBuff_ManaConsume : CitadelBuff {
	public override void Init()
	{
		foreach (HeroUnit unit in GameManager.Instance.heros) {
			if (null != unit.activeAttack) {
				unit.activeAttack.manaBuff -= this.Buff;
				unit.activeAttack.manaBuff += this.Buff;
			}
		}
	}
}
