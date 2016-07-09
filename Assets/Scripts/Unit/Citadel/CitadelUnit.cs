﻿using UnityEngine;
using System.Collections;

public class CitadelUnit : Unit {
    public AutoRecoveryInt hp;
	public AutoRecoveryInt mp;
    public int level;
	public int currentExp;
	public int nextExp {
		get { return level * level; }
	}
	public int sp;		// skill point
    public UnitSlot[] slots;

    void Start()
    {
        Init();
    }
    public override void Init()
	{
        base.Init();
		hp.max = 1000 + (level - 1) * 50;
		hp.value = hp.max;
		hp.interval = 1.0f;
		hp.recovery = 2;
	
		mp.max = 500 + (level -1) * 10;
		mp.value = mp.max;
		mp.interval = 1.0f;
		mp.recovery = 5;

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
		level += 1;
		Init ();
	}
}
