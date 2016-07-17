using UnityEngine;
using System.Collections;

public class UnitAttack_Summon : UnitAttack {
	public SummonedCreature creaturePrefab;
	public Vector3[] positions;
	public float health;
	public int level;
	public override void Attack()
	{
		foreach(Vector3 position in positions)
		{
			SummonedCreature creature = GameObject.Instantiate<SummonedCreature> (creaturePrefab);
			creature.transform.SetParent (GameManager.Instance.creatures);
			creature.transform.position = position;
			creature.level = level;
			creature.maxHealth = health + health * 0.2f * (level - 1);
			creature.attack.data.power = data.power;
		}
	}

	public override void Upgrade(int level)
	{
		this.level = level;
		base.Upgrade (level);
	}
}
