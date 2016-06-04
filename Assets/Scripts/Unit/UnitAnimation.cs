using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class UnitAnimation : MonoBehaviour {
	[HideInInspector]
	public Animator animator;
	[HideInInspector]
	public SpriteRenderer spriteRenderer;

	public delegate void AnimationEventDelegate();
	public Dictionary<string, AnimationEventDelegate> animationEvents;
	// Use this for initialization
	public void Init () {
		animationEvents = new Dictionary<string, AnimationEventDelegate> ();
		animator = GetComponent<Animator> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	public void AnimationEvent(string evt)
	{
		if (animationEvents.ContainsKey (evt)) {
			animationEvents [evt] ();
		}
	}
}