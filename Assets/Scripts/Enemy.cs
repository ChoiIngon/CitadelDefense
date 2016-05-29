using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour {
	public Vector3 direction;
	public float moveSpeed;
	public float attackSpeed;
	public float attackRange;
	public Rect size;
	public int hp;
	public int defense;

	public int rewardGold;
	public int rewardExp;

	//public string animationControllerPath;
	private Animator animator;
	// Use this for initialization

	void Start () {
		direction = Vector3.left;
		animator = GetComponent<Animator> ();
		//animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> (animationControllerPath);

		BoxCollider2D damageBox = GetComponent<BoxCollider2D> ();
		damageBox.offset = new Vector2(size.x, size.y);
		damageBox.size = new Vector2(size.width, size.height);
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
		if (state.IsName ("move")) {
			transform.Translate (direction * moveSpeed * Time.deltaTime);
		}
		if (state.IsName ("dead") && state.normalizedTime >= 1.0f)
		{
			DestroyImmediate (gameObject, true);
		}
	}

	void FixedUpdate() {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, attackRange);
		if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Citadel")) {
			animator.SetBool("isInAttackRange", true);
		}
	}

	public void Damage(int damage)
	{
		damage = Mathf.Max (damage - defense, 1);
		Effect_Damage damageEffect = GameObject.Instantiate<Effect_Damage> (Resources.Load<Effect_Damage> ("Prefabs/Effect_Damage"));
		damageEffect.transform.localPosition = transform.localPosition;
		damageEffect.Init (damage);
		hp = hp - damage;
		if (0 >= hp) {
			animator.SetTrigger ("isDead");
		}
	}
}
