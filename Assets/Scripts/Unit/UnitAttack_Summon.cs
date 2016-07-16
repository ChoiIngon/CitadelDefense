using UnityEngine;
using System.Collections;

public class UnitAttack_Summon : UnitAttack {
	public SummonedCreature creaturePrefab;
	public float health;
	public int level;
	public override void Attack()
	{
		SummonedCreature creature = GameObject.Instantiate<SummonedCreature> (creaturePrefab);
		creature.transform.SetParent (GameManager.Instance.creatures);
		creature.transform.localPosition = Vector3.zero;
		creature.level = level;
		//creature.transform.position = transform.position;
	}

	public override void Upgrade(int level)
	{
		this.level = level;
		base.Upgrade (level);
	}
}
