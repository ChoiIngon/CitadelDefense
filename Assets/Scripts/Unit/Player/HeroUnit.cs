using UnityEngine;
using System.Collections;

public class HeroUnit : BasePlayerUnit {
	public UnitAttack.AttackInfo normalAttackInfo;
	public UnitAttack.AttackInfo specialAttackInfo;
	public ProgressBar coolTimeBar;
	protected UnitAttack normalAttack;
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
