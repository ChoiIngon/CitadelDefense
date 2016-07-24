﻿using UnityEngine;
using System.Collections;

public class CitadelUnit : Unit {
	
    public int level;
	public int upgradeCost;

	[System.Serializable]
	public class UpgradeInfo
	{
		public int baseValue;
		public int upgradeValue;
		public float recoverySpeed;
		public float recoveryBonus;
	}

	public UpgradeInfo healthUpgradeInfo;
	public UpgradeInfo manaUpgradeInfo;
	public Item[] items = new Item[5];
	public CitadelParts[] citadelParts;
	public CitadelBuff[] citadelBuffs;

	[ReadOnly] public AutoRecoveryInt health;
	[ReadOnly] public AutoRecoveryInt mana;

	public void Init()
	{
		foreach (CitadelBuff buff in citadelBuffs) {
			buff.Init ();
		}

		health.max = healthUpgradeInfo.baseValue + (level - 1) * healthUpgradeInfo.upgradeValue;
		health.value = health.max;
		health.recovery = 1;
		health.interval = healthUpgradeInfo.recoverySpeed * Mathf.Max(1 + healthUpgradeInfo.recoveryBonus, 0.01f);

		mana.max = manaUpgradeInfo.baseValue + (level - 1) * manaUpgradeInfo.upgradeValue;
		mana.value = mana.max;
		mana.recovery = 1;
		mana.interval = manaUpgradeInfo.recoverySpeed * Mathf.Max(1 + manaUpgradeInfo.recoveryBonus, 0.01f);
	}

	public override void Damage(int damage)
	{
		health -= damage;
		if(0 >= health)
        {
			GameManager.Instance.WaveEnd (GameManager.WaveResult.Lose);
        }
	}

	public void Upgrade()
	{
		int cost = upgradeCost * level; 
		if (GameManager.Instance.gold < cost) {
			GameManager.Instance.uiMessageBox.message = "골드가 부족 합니다";
			return;
		}
		GameManager.Instance.gold -= cost;
		level += 1;
		Init ();
		if (citadelParts.Length >= level) {
			citadelParts [level - 1].gameObject.SetActive (true);
		}
		GameManager.Instance.Save ();
	}

    public void EquipItem(Item item)
    {
        item.equipped = true;
    }

    public void UnequipItem(Item item)
    {
    }
}
