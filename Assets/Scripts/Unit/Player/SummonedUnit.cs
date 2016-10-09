using UnityEngine;
using System.Collections;

public class SummonedUnit : Unit {
	[System.Serializable]
	public class UpgradeInfo
	{
		public float health;
	}
	public UpgradeInfo upgrade;
	public AutoRecoveryInt health;
	public ProgressBar healthBar;
	public float aliveTime;

	public void Init(int level)
	{
		health.max = (int)(health.max + upgrade.health * (level - 1));
		health.value = health.max;

		if (null != passiveAttack)
		{
			unitAnimation.animationEvents.Add("attack", passiveAttack.Attack);
			passiveAttack.self = this;
			passiveAttack.Upgrade(level);
		}
		unitMove.Init(transform.position, altitude);
	}
    void Update () {
		unitAnimation.spriteRenderer.sortingOrder = (int)(transform.position.y * -1000);
		EnemyUnit target = null;

		float minDistance = float.MaxValue;
		Transform enemyManager = GameManager.Instance.enemyManager.transform;
		for (int i = 0; i < enemyManager.childCount; i++) {
			EnemyUnit enemy = enemyManager.GetChild (i).GetComponent<EnemyUnit> ();
			if (null == enemy) {
				continue;
			}
			float distance = Vector3.Distance (transform.position, enemy.transform.position);
			if (0 < enemy.health && minDistance > distance) {
				target = enemy;
                passiveAttack.target = target;
				minDistance = distance;
			}
		}

		if (null != target) {
			if (target.transform.position.x < transform.position.x) {
				unitAnimation.transform.localScale = new Vector3 (-1.0f * Mathf.Abs (unitAnimation.transform.localScale.x), unitAnimation.transform.localScale.y, unitAnimation.transform.localScale.z);
			} else {
				unitAnimation.transform.localScale = new Vector3 (Mathf.Abs (unitAnimation.transform.localScale.x), unitAnimation.transform.localScale.y, unitAnimation.transform.localScale.z);
			}

			float distance = Vector3.Distance (transform.position, target.transform.position);

			if (distance > passiveAttack.data.maxRange) {
				unitAnimation.animator.SetTrigger ("move");
				unitAnimation.animator.speed = unitMove.speed;
				Vector3 targetPosition = target.transform.position;
				targetPosition.x = Mathf.Min (targetPosition.x, 9.5f);
				unitMove.Init(targetPosition, altitude);
			} else {
				unitAnimation.animator.SetTrigger ("attack");
				unitAnimation.animator.speed = passiveAttack.data.speed;
			}
		}
		healthBar.progress = (float)health.GetValue() / (float)health.max;
		aliveTime -= Time.deltaTime;
		if (0.0f >= aliveTime) {
			Destroy (gameObject);
		}
	}

	public override void Damage(int damage)
	{
		if (0 >= health) {
			return;
		}
		damage = Mathf.Max (damage, 1);
		health = health - damage;
		if (0 >= health) {
			Destroy (gameObject);
		}
	}
}
