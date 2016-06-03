using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	public Transform enemyManager;

	public float attackRange;
	public int attackPoint;
	public float attackSpeed;
	public Effect attackEffect;
	private float lastAttackTime;
	private Animator animator;
	private UnitAnimation unitAnimation;

	private Enemy target;
	void Start () {
		animator = transform.FindChild ("UnitAnimation").GetComponent<Animator> ();
		unitAnimation = transform.FindChild ("UnitAnimation").GetComponent<UnitAnimation> ();
		unitAnimation.animationEvent += AnimationEvent;
	}
	
	// Update is called once per frame
	void Update () {
		target = null;
		for (int i = 0; i < enemyManager.childCount; i++) {
			Enemy enemy = enemyManager.GetChild (i).GetComponent<Enemy>();
			float distance = Vector3.Distance (transform.position, enemy.transform.position);
			if (distance < attackRange) {
				target = enemy;
				break;
			}
		}

		if (null != target) {
			animator.SetTrigger ("attack");
			animator.speed = attackSpeed;
		} else {
			animator.SetTrigger ("idle");
			animator.speed = 1.0f;
		}


	}

	public void AnimationEvent(string evt)
	{
		if ("attack" == evt && null != target) {
			Effect effect = GameObject.Instantiate<Effect> (attackEffect);
			effect.transform.position = target.transform.position;
		}
	}
}
