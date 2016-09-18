using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	[System.Serializable]
	public class CitadelPartsSpriteInfo
	{
		public int level;
		public int partsIndex;
		public Sprite front;
		public Sprite back;
	};

	public UpgradeInfo healthUpgradeInfo;
	public UpgradeInfo manaUpgradeInfo;

	public CitadelParts[] citadelParts;
	public CitadelPartsSpriteInfo[] citadelPartsSpriteInfo;
	public CitadelBuff[] citadelBuffs;
	public Dictionary<int, HeroUnit> heros = new Dictionary<int, HeroUnit> ();

	[ReadOnly] public AutoRecoveryInt health;
	[ReadOnly] public AutoRecoveryInt mana;
	public void Init()
	{
		{
			Transform t = transform.FindChild ("Heros");
			for (int i = 0; i < t.childCount; i++) {
				HeroUnit hero = t.GetChild (i).GetComponent<HeroUnit> ();
				hero.gameObject.SetActive (false);
				heros [hero.info.id] = hero;
			}
		}

		{
			Transform t = transform.FindChild ("Animation/Parts");
			citadelParts = new CitadelParts[t.childCount];
			for (int i = 0; i < t.childCount; i++) {
				CitadelParts parts = t.GetChild (i).GetComponent<CitadelParts> ();
				parts.Init ();
				parts.gameObject.SetActive (false);
				citadelParts [parts.slotIndex] = parts;
			}
		}
	}
	public void Reset()
	{
		foreach (CitadelPartsSpriteInfo info in citadelPartsSpriteInfo) {
			if (info.level <= level) {
				CitadelParts parts = citadelParts [info.partsIndex];
				parts.gameObject.SetActive (true);
				parts.front.sprite = info.front;
				parts.back.sprite = info.back;
			} else {
				break;
			}
		}

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
        Hashtable ht = new Hashtable();
        ht.Add("x", 0.05f);
        ht.Add("y", 0.05f);
        ht.Add("time", 0.3f);
        iTween.ShakePosition(gameObject, ht);
        
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
		Reset ();
		if (citadelParts.Length >= level) {
			citadelParts [level - 1].gameObject.SetActive (true);
		}
		GameManager.Instance.Save ();
	}
}
