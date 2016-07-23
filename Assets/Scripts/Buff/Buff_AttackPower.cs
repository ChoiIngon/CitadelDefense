using UnityEngine;
using System.Collections;

public class Buff_AttackPower : Buff {
	public float value;
	public float upgradeValue;

	public float time;
	private float deltaTime;
	// Use this for initialization
	public override void Start () {
		base.Start();
		if(null == unit)
		{
			return;
		}
		deltaTime = 0.0f;
		unit.passiveAttack.powerBuff += AttackPower;
	}

	void AttackPower(ref float ret, float original)
	{
		ret += original * value;
	}

	// Update is called once per frame
	void Update () {
		deltaTime += Time.deltaTime;
		if (deltaTime > time) {
			unit.passiveAttack.powerBuff -= AttackPower;
			Destroy (gameObject);		
		}
	}
}
