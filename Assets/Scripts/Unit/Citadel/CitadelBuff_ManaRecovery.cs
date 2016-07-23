using UnityEngine;
using System.Collections;

public class CitadelBuff_ManaRecovery : CitadelBuff {
	public override void Init()
	{
		GameManager.Instance.citadel.manaUpgradeInfo.recoveryBonus = value;
	}
	public override void Upgrade()
	{
	}
}
