using UnityEngine;
using System.Collections;

public class CitadelBuff_Cooltime : CitadelBuff {
	void Start()
	{
		//value = -0.01f * level;
	}
	public override void Init()
	{
		foreach (var itr in GameManager.Instance.citadel.heros) {
			HeroUnit unit = itr.Value;
			if (null != unit.activeAttack) {
				unit.activeAttack.cooltimeBuff -= this.Buff;
				unit.activeAttack.cooltimeBuff += this.Buff;
			}
		}
	}
}
