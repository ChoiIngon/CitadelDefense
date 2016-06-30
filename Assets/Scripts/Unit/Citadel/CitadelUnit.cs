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
    public ProgressBar hpBar;
    public ProgressBar mpBar;
	public ProgressBar expBar;
	public ProgressBar timeBar; // show wave progress

    void Start()
    {
        Init();
    }
    public override void Init()
	{
        base.Init();
		hp.max = 1000 + level * 50;
		hp.value = hp.max;
		hp.interval = 1.0f;
		hp.recovery = 5;
	
		mp.max = 500 + level * 10;
		mp.value = mp.max;
		mp.interval = 1.0f;
		mp.recovery = 5;
	}
	// Update is called once per frame
	void Update () {
        hpBar.progress = (float)hp.value / (float)hp.max;
        mpBar.progress = (float)mp.value / (float)mp.max;
	}

	public override void Damage(int damage)
	{
		hp -= damage;
        if(0 >= hp)
        {
			GameManager.Instance.WaveEnd (GameManager.WaveResult.Lose);
        }
	}
}
