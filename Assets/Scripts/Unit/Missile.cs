﻿using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	[HideInInspector]
	public UnitAttack attack;
	public UnitHit hitPrefab;
	protected UnitMove move;
	// Use this for initialization
	public void Init (Vector3 start, Vector3 end, UnitAttack attack, float altitude = 0.0f) {
		transform.position = start;
		move = GetComponent<UnitMove> ();
		move.Init (end, altitude);
		this.attack = attack;
	}

	void Start()
	{
		move = GetComponent<UnitMove> ();
	}

	// Update is called once per frame
	void Update () {
		if (1.0f <= move.interpolate)
		{
            UnitHit hit = GameObject.Instantiate<UnitHit>(hitPrefab);
			hit.attack = attack;
			hit.transform.position = move.end;
			DestroyObject (gameObject);
		}
	}
}
