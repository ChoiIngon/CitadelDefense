using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class UnitAnimation : MonoBehaviour {
	public Animator animator;
	public SpriteRenderer spriteRenderer;
	public delegate void AnimationEventDelegate(string evt);

	public AnimationEventDelegate animationEvent;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	public void AnimationEvent(string evt)
	{
		animationEvent (evt);
	}
}
