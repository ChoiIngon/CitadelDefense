using UnityEngine;
using System.Collections;

public class CitadelBuff_Cooltime : CitadelBuff {
	void Start()
	{
		//value = -0.01f * level;
	}
	public override void Init()
	{
		foreach (HeroUnit unit in GameManager.Instance.heros) {
			if (null != unit.activeAttack) {
				unit.activeAttack.cooltimeBuff -= this.Buff;
				unit.activeAttack.cooltimeBuff += this.Buff;
			}
		}
	}
	public override void Upgrade()
	{
		maxLevel = 20;
		if (level >= maxLevel + 1) {
			return;
		}

		level++;
		value = -0.01f * level;
	}
}
