using UnityEngine;
using System.Collections;

public class CitadelUnit : Unit {
	public AutoRecoveryInt health;
	public AutoRecoveryInt mana;

    public int level;
	public int baseUpgradeCost;
	public int baseHeath;
	public int upgradeHealth;
	public int baseMana;
	public int upgradeMana;

	public UnitSlot[] slots;
	public Item[] items = new Item[5];

    public void Init()
	{
		health.max = baseHeath + (level - 1) * upgradeHealth;
		health.value = health.max;

		mana.max = baseMana + (level -1) * upgradeMana;
		mana.value = mana.max;

		if (slots.Length >= level) {
			slots [level - 1].gameObject.SetActive (true);
		}
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
		int cost = baseUpgradeCost * level; 
		if (GameManager.Instance.gold < cost) {
			GameManager.Instance.uiMessageBox.message = "골드가 부족 합니다";
			return;
		}
		GameManager.Instance.gold -= cost;
		level += 1;
		Init ();
	}

    public void EquipItem(Item item)
    {
        item.equipped = true;
    }

    public void UnequipItem(Item item)
    {
    }
}
