﻿using UnityEngine;
using System.Collections;

public class UnitAttack_Summon : UnitAttack {
	public SummonedUnit creaturePrefab;
	public Vector3[] positions;
	private int level;
	public override void Attack()
	{
		foreach(Vector3 position in positions)
		{
            SummonedUnit creature = GameObject.Instantiate<SummonedUnit> (creaturePrefab);
			creature.transform.SetParent (GameManager.Instance.creatures);
			creature.transform.position = position;
			creature.Init (level);
		}
	}

	public override void Upgrade(int level)
	{
		this.level = level;
		base.Upgrade (level);
	}
}
