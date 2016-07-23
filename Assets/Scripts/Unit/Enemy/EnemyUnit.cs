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
	public AutoRecoveryInt hp;
    public float gold;
	public static float BOUNUS_GOLD;
    public float exp;    
	public UpgradeInfo upgrade;
    public Vector3 direction;

	public ProgressBar healthBar;
    public Effect_Damage effectDamage;

	public override void Start () {
		base.Start ();
		hp.max = (int)(hp.max + upgrade.health * (GameManager.Instance.waveLevel - 1));
		hp.value = hp.max;
	    
		gold = gold + upgrade.gold * (GameManager.Instance.waveLevel - 1);

        if (null != passiveAttack)
        {
            unitAnimation.animationEvents.Add("attack", passiveAttack.Attack);
            passiveAttack.self = this;
            passiveAttack.Upgrade(GameManager.Instance.waveLevel);
        }
    }

	void Update () {
		if (0 < hp.max) {
			healthBar.progress = (float)hp.GetValue () / (float)hp.max;
		}
		unitAnimation.spriteRenderer.sortingOrder = (int)(transform.position.y * -1000);

        if (0 < hp)
        {
            Debug.DrawRay(transform.position, Vector3.left * passiveAttack.data.maxRange, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, passiveAttack.data.maxRange, 1 << LayerMask.NameToLayer("Citadel"));
            if(null != hit.collider)
            {
                UnitColliderDamage colDamage = hit.collider.GetComponent<UnitColliderDamage>();
                if (null != colDamage)
                {
                    passiveAttack.target = colDamage.unit;
                }
                unitAnimation.animator.SetTrigger("attack");
                if (null != unitMove.buff)
                {
					unitAnimation.animator.speed = unitMove.speed;
                }
                else
                {
                    unitAnimation.animator.speed = passiveAttack.data.speed;
                }
                actionState = ActionState.Attack;
            }
            else
            {
                unitAnimation.animator.SetTrigger("move");
				unitAnimation.animator.speed = unitMove.speed;
                actionState = ActionState.Move;
            }

        }
        AnimatorStateInfo state = unitAnimation.animator.GetCurrentAnimatorStateInfo(0);
		if (ActionState.Move == actionState) {
			unitMove.enabled = true;
			unitMove.Init (transform.position + Vector3.left, altitude);
			//transform.Translate (direction * unitMove.speed * Time.deltaTime);
		} else {
			unitMove.enabled = false;
		}
			
		if (state.IsName("dead") && state.normalizedTime >= 1.0f)
		{
			DestroyImmediate (gameObject, true);
		}
    }

	public override void Damage(int damage)
	{
		if (0 >= hp) {
			return;
		}

		GameObject go = new GameObject ();
		go.name = "Effect_Damage";
		go.transform.position = transform.position;
        Effect_Damage effect = (Effect_Damage)GameObject.Instantiate<Effect_Damage>(effectDamage);
		effect.transform.SetParent (go.transform);
        effect.Init(damage);
		hp -= damage;
		if (0 >= hp) {
            actionState = ActionState.Dead;
			unitAnimation.animator.SetTrigger ("dead");
			unitAnimation.animator.speed = 1.0f;
			healthBar.gameObject.SetActive (false);
			GameManager.Instance.gold += (int)gold + (int)(gold * BOUNUS_GOLD);
		}
	}
}
