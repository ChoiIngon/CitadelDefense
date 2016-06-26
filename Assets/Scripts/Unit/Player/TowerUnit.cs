using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnitAnimation))]
public class TowerUnit : BasePlayerUnit {
	private EnemyUnit target;
    public AnimationClip idleAnimationClip;
    public AnimationClip attackAnimationClip;
	public UnitAttack.AttackInfo attackInfo;
	protected void Start () {
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

		unitAttack.info = attackInfo;
		unitAttack.self = this;
	}
	
	// Update is called once per frame
	void Update () {
		target = null;
		Transform enemyManager = GameManager.Instance.enemyManager.transform;
		for (int i = 0; i < enemyManager.childCount; i++) {
			EnemyUnit enemy = enemyManager.GetChild (i).GetComponent<EnemyUnit>();
			if (null == enemy) {
				continue;
			}
			float distance = Vector3.Distance (transform.position, enemy.transform.position);
			if (distance < unitAttack.info.range) {
				target = enemy;
				unitAttack.target = target;
				break;
			}
		}

		if (null != target) {
			unitAnimation.animator.SetTrigger ("attack");
			unitAnimation.animator.speed = unitAttack.info.speed;
		} else {
			unitAnimation.animator.SetTrigger ("idle");
			unitAnimation.animator.speed = 1.0f;
		}
	}
}
