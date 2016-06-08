using UnityEngine;
using System.Collections;

public class UnitHero : UnitTurret {
	public int price;

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
