using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnitAnimation))]
public class UnitTurret : Unit {
	public Transform enemyManager;

	private UnitEnemy target;

    public AnimationClip idleAnimationClip;
    public AnimationClip attackAnimationClip;
	void Start () {
		base.Start ();
        if (null != idleAnimationClip)
        {
            unitAnimation.ChangeAnimationClip("idle", idleAnimationClip);
        }
        if (null != attackAnimationClip)
        {
            unitAnimation.ChangeAnimationClip("attack", attackAnimationClip);
        }

		unitAnimation.animationEvents.Add ("attack", unitAttack.Attack);
	}
	
	// Update is called once per frame
	void Update () {
		target = null;
		for (int i = 0; i < enemyManager.childCount; i++) {
			UnitEnemy enemy = enemyManager.GetChild (i).GetComponent<UnitEnemy>();
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
