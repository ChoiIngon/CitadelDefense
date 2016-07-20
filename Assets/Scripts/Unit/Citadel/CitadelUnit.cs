using UnityEngine;
using System.Collections;

public class CitadelUnit : Unit {
	public AutoRecoveryInt hp;
	public AutoRecoveryInt mp;

    public int level;
	public UnitSlot[] slots;

    public void Init()
	{
		hp.max = 1000 + (level - 1) * 50;
		hp.value = hp.max;
		hp.interval = 1.0f;
		hp.recovery = 2;
	
		mp.max = 500 + (level -1) * 10;
		mp.value = mp.max;
		mp.interval = 1.0f;
		mp.recovery = 1;

		if (slots.Length >= level) {
			slots [level - 1].gameObject.SetActive (true);
		}
	}

	public override void Damage(int damage)
	{
		hp -= damage;
        if(0 >= hp)
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
