﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnitAnimation))]
public class EnemyUnit : Unit {
    [System.Serializable]
    public class LevelupInfo : UnitAttack.AttackData
    {
        public float health;
        public float gold;
        public float exp;
    }
    public enum ActionState
    {
        Move,
        Attack,
        Dead
    }
    public ActionState actionState = ActionState.Move;
    public int firstWave;

	public float health;
	public float maxHealth;
    public float rewardGold;
    public float rewardExp;
    public float moveSpeed;
    public Vector3 direction;

	public ProgressBar healthBar;
    public Effect_Damage effectDamage;

    public AnimationClip moveAnimationClip;
    public AnimationClip attackAnimationClip;
    public AnimationClip deadAnimationClip;
	public UnitAttack.AttackInfo attackInfo;
    public LevelupInfo levelupInfo;

	public override void Init () {
		base.Init ();
		healthBar = transform.FindChild("HealthBar").GetComponent<ProgressBar>();

        if(null != moveAnimationClip)
        {
            unitAnimation.ChangeAnimationClip("move", moveAnimationClip);
        }

        if(null != attackAnimationClip)
        {
            unitAnimation.ChangeAnimationClip("attack", attackAnimationClip);
        }

        unitAnimation.animationEvents.Add("attack", unitAttack.Attack);

        if(null != deadAnimationClip)
        {
            unitAnimation.ChangeAnimationClip("dead", deadAnimationClip);
        }

		unitAttack.info = attackInfo;
        unitAttack.self = this;

		maxHealth = maxHealth + (maxHealth * levelupInfo.health * (GameManager.Instance.waveLevel - 1));
		health = maxHealth;
        unitAttack.data.power = unitAttack.info.power + (unitAttack.info.power * levelupInfo.power * (GameManager.Instance.waveLevel - 1));
        unitAttack.data.maxRange = unitAttack.info.maxRange + (unitAttack.info.maxRange * levelupInfo.maxRange * (GameManager.Instance.waveLevel - 1));
        unitAttack.data.speed = unitAttack.info.speed + (unitAttack.info.speed * levelupInfo.speed * (GameManager.Instance.waveLevel - 1));
    }

	void Update () {
        healthBar.progress = (float)health / (float)maxHealth;
        if (0 < health)
        {
            Debug.DrawRay(transform.position, Vector3.left * unitAttack.info.maxRange, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, unitAttack.info.maxRange, 1 << LayerMask.NameToLayer("Citadel"));
            if (null == hit.collider)
            {
                unitAnimation.animator.SetTrigger("move");
                unitAnimation.animator.speed = 1.0f;
                actionState = ActionState.Move;
            }
            else
            {
                UnitColliderDamage colDamage = hit.collider.GetComponent<UnitColliderDamage>();
                if (null != colDamage)
                {
                    unitAttack.target = colDamage.unit;
                }
                unitAnimation.animator.SetTrigger("attack");
                unitAnimation.animator.speed = unitAttack.info.speed;
                actionState = ActionState.Attack;
            }
        }
        AnimatorStateInfo state = unitAnimation.animator.GetCurrentAnimatorStateInfo(0);
		if (ActionState.Move == actionState) {
			transform.Translate (direction * moveSpeed * Time.deltaTime);
			unitAnimation.animator.speed = 1.0f;
		}
			
		if (ActionState.Dead == actionState && state.normalizedTime >= 1.0f)
		{
			DestroyImmediate (gameObject, true);
		}
    }

	public override void Damage(int damage)
	{
		if (0 >= health) {
			return;
		}

		GameObject go = new GameObject ();
		go.name = "Effect_Damage";
		go.transform.position = transform.position;
        Effect_Damage effect = (Effect_Damage)GameObject.Instantiate<Effect_Damage>(effectDamage);
		effect.transform.SetParent (go.transform);
        effect.Init(damage);
		health = health - damage;
		if (0 >= health) {
            actionState = ActionState.Dead;
			unitAnimation.animator.SetTrigger ("dead");
			healthBar.gameObject.SetActive (false);
			GameManager.Instance.gold += (int)rewardGold;
		}
	}
}
