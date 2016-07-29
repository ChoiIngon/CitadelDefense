using UnityEngine;
using System.Collections;

public class CitadelBuff_ManaConsume : CitadelBuff {
	public override void Init()
	{
		foreach (var itr in GameManager.Instance.citadel.heros) {
			HeroUnit unit = itr.Value;
			if (null != unit.activeAttack) {
				unit.activeAttack.manaBuff -= this.Buff;
				unit.activeAttack.manaBuff += this.Buff;
			}
		}
	}
}
