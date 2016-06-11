using UnityEngine;
using System.Collections;

public class UnitHero : UnitTurret {
	public int price;
	public UnitAttack.Info normalAttackInfo;
	public UnitAttack.Info specialAttackInfo;

	protected UnitAttack specialAttack;
	// Use this for initialization
	void Start () {
		base.Start ();
		{
			Transform tr = transform.FindChild ("SpecialAttack");
			if (null != tr) {
				specialAttack = tr.GetComponent<UnitAttack> ();
				if (null != specialAttack) {
					specialAttack.info = specialAttackInfo;
				}
			}
		}
		/*
		transform.FindChild("TouchEvent").GetComponent<TouchEvent>().onEvent += () =>
		{

		};
		*/ 
	}
}
