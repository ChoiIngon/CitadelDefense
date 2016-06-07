using UnityEngine;
using System.Collections;

public class UnitCitadel : Unit {
    
    public AutoRecoveryInt hp;
	public AutoRecoveryInt mp;
    private int _level;
	public int level;
    private int _exp;
	public int exp;
    private int _gold;
    public int gold;
    public ProgressBar hpBar;
    public ProgressBar mpBar;
	public Transform [] turretSlot;

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
            GameManager.Instance.failPopup.SetActive(true);
        }
	}
}
