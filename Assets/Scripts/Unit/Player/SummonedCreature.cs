using UnityEngine;
using System.Collections;

public class SummonedCreature : Unit {
	
	public float moveSpeed;
	public int maxHealth;
	public int health;
	public ProgressBar healthBar;
	// Use this for initialization
	void Start () {
		unitAttack.self = this;
		unitAnimation.animationEvents.Add ("attack", unitAttack.Attack);
	}
	
	void Update () {
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
				unitAttack.target = target;
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

			if (distance > unitAttack.data.maxRange) {
				unitAnimation.animator.SetTrigger ("move");
				unitAnimation.animator.speed = 1.0f;
				transform.position = Vector3.Lerp (transform.position, target.transform.position, moveSpeed * Time.deltaTime);
			} else {
				unitAnimation.animator.SetTrigger ("attack");
				unitAnimation.animator.speed = unitAttack.data.speed;
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
