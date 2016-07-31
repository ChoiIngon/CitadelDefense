using UnityEngine;
using System.Collections;
public class EnemyHeroUnit : Unit {
	public HeroUnit.Info info;

	public int	level;
	[ReadOnly] public int	slotIndex; 
	[ReadOnly] public bool	purchased;
	[ReadOnly] public bool	equiped;
	[ReadOnly] public float height;

	private float coolTime;
	public ProgressBar coolTimeBar;

	public override void Start() {
		base.Start ();
		if (null != passiveAttack)
		{
			unitAnimation.animationEvents.Add("attack", passiveAttack.Attack);
			passiveAttack.self = this;
		}

		Init ();
	}
	public void Init() {
		if (null != coolTimeBar) {
			coolTimeBar.progress = 1.0f;
			coolTime = 0.0f;
		}
		if (null != passiveAttack) {
			passiveAttack.Upgrade(level);
		}
		if (null != activeAttack) {
			activeAttack.Upgrade(level);
		}
	}
	void Update () {
		/*
		EnemyUnit target = null;
		float minDistance = float.MaxValue;

		Transform enemyManager = GameManager.Instance.enemyManager.transform;
		for (int i = 0; i < enemyManager.childCount; i++) {
			EnemyUnit enemy = enemyManager.GetChild (i).GetComponent<EnemyUnit>();
			if (null == enemy) {
				continue;
			}
			float distance = Mathf.Abs(transform.position.x - enemy.transform.position.x);
			if (distance > passiveAttack.data.minRange && distance < passiveAttack.data.maxRange && 0 < enemy.hp && minDistance > distance) {
				target = enemy;
				minDistance = distance;
			}
		}

		if (null != target) {
			unitAnimation.animator.SetTrigger ("attack");
			passiveAttack.target = target;
			unitAnimation.animator.speed = passiveAttack.data.speed;
		} else {
			unitAnimation.animator.SetTrigger ("idle");
			unitAnimation.animator.speed = 1.0f;
		}
		if (0 < coolTime && null != coolTimeBar) {
			coolTime -= Time.deltaTime;
			coolTimeBar.progress = (activeAttack.data.cooltime - coolTime) / activeAttack.data.cooltime;
		}
		*/
	}
}
