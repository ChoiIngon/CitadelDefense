using UnityEngine;
using System.Collections;

public class SummonedCreature : Unit {
	public int level;
	public float maxHealth;
	public float health;
	public ProgressBar healthBar;
	public UnitAttack attack;
	// Use this for initialization
	void Start () {
		maxHealth = maxHealth + (maxHealth * 0.1f * (level - 1));
		health = maxHealth;
		attack.Upgrade (level);
		unitAnimation.animationEvents.Add ("attack", attack.Attack);
        unitMove.Init(transform.position, transform.position);
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
			if (0 < enemy.hp && minDistance > distance) {
				target = enemy;
				attack.target = target;
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

			if (distance > attack.data.maxRange) {
				unitAnimation.animator.SetTrigger ("move");
				unitAnimation.animator.speed = unitMove.speed;
				Vector3 targetPosition = target.transform.position;
				targetPosition.x = Mathf.Min (targetPosition.x, 9.5f);
				unitMove.Init(transform.position, targetPosition);
			} else {
				unitAnimation.animator.SetTrigger ("attack");
				unitAnimation.animator.speed = attack.data.speed;
			}
		}
		healthBar.progress = (float)health / (float)maxHealth;
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
