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
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Upgrade () {
		level += 1;

	}
}
