﻿using UnityEngine;
using System.Collections;

public class UnitEnemy : Unit {
	public Vector3 direction;
	public float moveSpeed;
	public UnitCitadel citadel;
	public int health;
	public int maxHealth;
	[HideInInspector]
	ProgressBar healthBar;

	public int defense;

	public int rewardGold;
	public int rewardExp;

	void Start () {
		base.Start ();
		healthBar = transform.FindChild("HealthBar").GetComponent<ProgressBar>();
		unitAnimation.animationEvents.Add ("attack", unitAttack.Attack);
	}

	void Update () {
		if (0 < maxHealth) {
			healthBar.progress = (float)health / (float)maxHealth;
		}

		AnimatorStateInfo state = unitAnimation.animator.GetCurrentAnimatorStateInfo(0);
		if (state.IsName ("move")) {
			transform.Translate (direction * moveSpeed * Time.deltaTime);
			unitAnimation.animator.speed = 1.0f;
		}
			
		if (state.IsName ("dead") && state.normalizedTime >= 1.0f)
		{
			DestroyImmediate (gameObject, true);
		}
	}

	void FixedUpdate()
	{
		Debug.DrawRay(transform.position, Vector3.left * unitAttack.range, Color.red);

		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, unitAttack.range, 1 << LayerMask.NameToLayer("Citadel"));
		if (hit.collider != null)
		{
			moveSpeed = 0.0f;
			unitAnimation.animator.SetTrigger ("attack");
			unitAnimation.animator.speed = unitAttack.speed;
		}
	}

	public override void Damage(int damage)
	{
		if (0 >= health) {
			return;
		}
		damage = Mathf.Max (damage - defense, 1);
		GameObject go = new GameObject ();
		go.name = "Effect_Damage";
		go.transform.position = transform.position;
		Effect_Damage damageEffect = GameObject.Instantiate<Effect_Damage> (Resources.Load<Effect_Damage> ("Prefabs/Effect_Damage"));
		damageEffect.transform.SetParent (go.transform);
		damageEffect.Init (damage);
		health = health - damage;
		if (0 >= health) {
			unitAnimation.animator.SetTrigger ("dead");
		}
	}
}
