using UnityEngine;
using System.Collections;

public class HeroUnit : TowerUnit {
	public ProgressBar coolTimeBar;

	// Use this for initialization
	public override void Init() {
		base.Init ();
		/*
		transform.FindChild("TouchEvent").GetComponent<TouchEvent>().onEvent += () =>
		{

		};
		*/ 
	}
}
