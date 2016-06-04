using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnitAnimation))]
public class UnitTurret : Unit {
	public Transform enemyManager;
	private UnitAttack unitAttack;
	private UnitAnimation unitAnimation;

	private Enemy target;
	void Start () {
		unitAttack = GetComponent<UnitAttack> ();
		if (null == unitAttack) {
			throw new System.Exception ("UnitAttack Component should be attached");
		}
		unitAnimation = GetComponent<UnitAnimation> ();

		unitAnimation.Init ();
		unitAnimation.animationEvents.Add ("attack", unitAttack.Attack);
	}
	
	// Update is called once per frame
	void Update () {
		target = null;
		for (int i = 0; i < enemyManager.childCount; i++) {
			Enemy enemy = enemyManager.GetChild (i).GetComponent<Enemy>();
			float distance = Vector3.Distance (transform.position, enemy.transform.position);
			if (distance < unitAttack.range) {
				target = enemy;
				unitAttack.start = transform.position;
				unitAttack.target = target.transform.position;
				break;
			}
		}

		if (null != target) {
			unitAnimation.animator.SetTrigger ("attack");
			unitAnimation.animator.speed = unitAttack.speed;
		} else {
			unitAnimation.animator.SetTrigger ("idle");
			unitAnimation.animator.speed = 1.0f;
		}
	}
}
