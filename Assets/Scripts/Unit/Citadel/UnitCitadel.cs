using UnityEngine;
using System.Collections;

public class UnitCitadel : Unit {
    public AutoRecoveryInt hp;
	public AutoRecoveryInt mana;
	public int level;
	public int exp;
    public ProgressBar healthBar;

	public Transform [] turretSlot;

	// Update is called once per frame
	void Update () {
        healthBar.progress = (float)hp.value / (float)hp.max;
	}

	public override void Damage(int damage)
	{
		hp -= damage;
	}
}
