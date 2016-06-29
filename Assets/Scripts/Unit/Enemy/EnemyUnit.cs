using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnitAnimation))]
public class EnemyUnit : Unit {
	public int baseHealth;
	public int baseAttackPower;
	public int firstWave;
	public float upgradeHealth;
	public float upgradeAttackPower;
	public int baseRewardGold;
	public int baseRewardExp;
	public float upgradeRewardGold;
	public float upgradeRewardExp;

	public Vector3 direction;
	public float moveSpeed;

	public int health;
	public int maxHealth;


	[HideInInspector]
	ProgressBar healthBar;
    public Effect_Damage effectDamage;

	public int defense;



    public AnimationClip moveAnimationClip;
    public AnimationClip attackAnimationClip;
    public AnimationClip deadAnimationClip;
	public UnitAttack.AttackInfo attackInfo;
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
		Debug.DrawRay(transform.position, Vector3.left * unitAttack.info.range, Color.red);

		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, unitAttack.info.range, 1 << LayerMask.NameToLayer("Citadel"));
		if (hit.collider != null)
		{
			moveSpeed = 0.0f;
			unitAnimation.animator.SetTrigger ("attack");
			unitAnimation.animator.speed = unitAttack.info.speed;
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
        Effect_Damage effect = (Effect_Damage)GameObject.Instantiate<Effect_Damage>(effectDamage);
		effect.transform.SetParent (go.transform);
        effect.Init(damage);
		health = health - damage;
		if (0 >= health) {
			unitAnimation.animator.SetTrigger ("dead");
		}
	}
}
