using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(UnitAnimation))]
[RequireComponent (typeof (UnitColliderAttack))]
public class UnitHit_CircleRange : UnitHit {
	private Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		GetComponent<UnitColliderAttack> ().power = power;
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo (0);
		if (state.normalizedTime >= 1.0f)
		{
			DestroyImmediate (gameObject, true);
		}
	}
}
