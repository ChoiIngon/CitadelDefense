using UnityEngine;
using System.Collections;

public class UnitAttack_Summon : UnitAttack {
	public SummonedCreature creaturePrefab;

	public override void Attack()
	{
		SummonedCreature creature = GameObject.Instantiate<SummonedCreature> (creaturePrefab);
		creature.transform.SetParent (GameManager.Instance.creatures);
		creature.transform.localPosition = Vector3.zero;
		//creature.transform.position = transform.position;
	}
}
