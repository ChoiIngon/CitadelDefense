using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour {
    public Citadel citadel;
	public Vector3 direction;
	public float moveSpeed;
    
	public float attackSpeed;
	public float attackRange;
    float lastAttackTime;
	
	public int health;
    public int maxHealth;
    ProgressBar healthBar;

	public int defense;

	public int rewardGold;
	public int rewardExp;

    
	//public string animationControllerPath;
	private Animator animator;
	// Use this for initialization

	void Start () {
        lastAttackTime = 0.0f;
		direction = Vector3.left;
		animator = transform.FindChild("UnitSprite").GetComponent<Animator> ();
		//animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> (animationControllerPath);
        		
        healthBar = transform.FindChild("HealthBar").GetComponent<ProgressBar>();
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.progress = (float)health / (float)maxHealth;
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
		if (state.IsName ("move")) {
			transform.Translate (direction * moveSpeed * Time.deltaTime);
		}

        if(state.IsName("attack"))
        {
            if (attackSpeed < Time.realtimeSinceStartup - lastAttackTime)
            {
                Attack();
                lastAttackTime = Time.realtimeSinceStartup;
            }
        }

		if (state.IsName ("dead") && state.normalizedTime >= 1.0f)
		{
			DestroyImmediate (gameObject, true);
		}
	}

    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector3.left * attackRange, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, attackRange, 1 << LayerMask.NameToLayer("Citadel"));
        if (hit.collider != null)
        {
            moveSpeed = 0.0f;
			animator.SetTrigger ("attack");
        }
    }
	public void Damage(int damage)
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
			animator.SetTrigger ("dead");
		}
	}
    public virtual void Attack()
    {

    }
}
