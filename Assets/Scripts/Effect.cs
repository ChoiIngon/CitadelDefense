using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
[RequireComponent (typeof (Animator))]
public class Effect : MonoBehaviour {
	protected Animator animator;
	public string stateName;

	// Use this for initialization
	protected void Start () {
		animator = GetComponent<Animator> ();
	}

	protected void Update () {
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo (0);
		if (state.IsName(stateName) && state.normalizedTime >= 1.0f)
		{
			DestroyImmediate (gameObject, true);
		}
	}
}
