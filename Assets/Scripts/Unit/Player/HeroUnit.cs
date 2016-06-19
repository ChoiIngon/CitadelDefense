using UnityEngine;
using System.Collections;

public class HeroUnit : TowerUnit {
	public ProgressBar coolTimeBar;

	// Use this for initialization
	void Start () {
		base.Start ();
		/*
		transform.FindChild("TouchEvent").GetComponent<TouchEvent>().onEvent += () =>
		{

		};
		*/ 
	}
}
