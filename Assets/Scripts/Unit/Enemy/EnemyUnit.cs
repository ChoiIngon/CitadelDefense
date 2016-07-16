using UnityEngine;
using System.Collections;

public class EnemyUnit : Unit {
	[System.Serializable]
	public class UpgradeInfo
	{
		public float health;
		public float gold;
	}
    public enum ActionState
    {
        Move,
        Attack,
        Dead
    }

    public ActionState actionState = ActionState.Move;
    
	public float health;
	public float maxHealth;
    public float gold;
    public float exp;
    public float speed;
	public UpgradeInfo upgrade;
    public Vector3 direction;

	public ProgressBar healthBar;
    public Effect_Damage effectDamage;

	public UnitAttack attack;
	public UnitAnimation unitAnimation;

	void Start () {
	    unitAnimation.animationEvents.Add("attack", attack.Attack);
		maxHealth = maxHealth + upgrade.health * (GameManager.Instance.waveLevel - 1);
		health = maxHealth;
		gold = gold + upgrade.gold * (GameManager.Instance.waveLevel - 1);
		attack.Upgrade (GameManager.Instance.waveLevel);
    }

	void Update () {
        healthBar.progress = (float)health / (float)maxHealth;
        if (0 < health)
        {
            Debug.DrawRay(transform.position, Vector3.left * attack.data.maxRange, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, attack.data.maxRange, 1 << LayerMask.NameToLayer("Citadel"));
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
                    attack.target = colDamage.unit;
                }
                unitAnimation.animator.SetTrigger("attack");
                unitAnimation.animator.speed = attack.data.speed;
                actionState = ActionState.Attack;
            }
        }
        AnimatorStateInfo state = unitAnimation.animator.GetCurrentAnimatorStateInfo(0);
		if (ActionState.Move == actionState) {
			transform.Translate (direction * speed * Time.deltaTime);
			unitAnimation.animator.speed = 1.0f;
		}
			
		if (state.IsName("dead") && state.normalizedTime >= 1.0f)
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
			GameManager.Instance.gold += (int)gold;
		}
	}
}
