﻿using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	[HideInInspector]
	public UnitAttack attack;
	protected UnitMove move;
	// Use this for initialization
	public void Init (Vector3 start, Vector3 end, UnitAttack attack, float altitude = 0.0f) {
		transform.position = start;
		this.attack = attack;
		move = GetComponent<UnitMove> ();
		move.Init (end, altitude);
	}

	void Start()
	{
		move = GetComponent<UnitMove> ();
	}

	// Update is called once per frame
	void Update () {
		if (1.0f <= move.interpolate)
		{
			attack.Hit (move.end);
            Object.Destroy(gameObject);
		}
	}
}
