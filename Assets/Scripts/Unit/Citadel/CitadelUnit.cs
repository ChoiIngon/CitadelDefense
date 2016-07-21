using UnityEngine;
using System.Collections;

public class CitadelUnit : Unit {
	public AutoRecoveryInt health;
	public AutoRecoveryInt mana;

    public int level;
	public UnitSlot[] slots;

    public void Init()
	{
		health.max = 1000 + (level - 1) * 50;
		health.value = health.max;
		health.interval = 1.0f;
		health.recovery = 2;
	
		mana.max = 500 + (level -1) * 10;
		mana.value = mana.max;
		mana.interval = 1.0f;
		mana.recovery = 1;

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
		if (GameManager.Instance.gold < 2000 * level) {
			GameManager.Instance.uiMessageBox.message = "골드가 부족 합니다";
			return;
		}
		GameManager.Instance.gold -= 2000 * level;
		level += 1;
		Init ();
	}
}
