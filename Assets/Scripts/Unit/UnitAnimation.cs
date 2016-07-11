using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class UnitAnimation : MonoBehaviour {
	[HideInInspector]
	public Animator animator {
		get {
			if (null == _animator) {
				_animator = GetComponent<Animator> ();
			}
			return _animator;
		}
	}
	private Animator _animator;
	[HideInInspector]
	public SpriteRenderer spriteRenderer{
		get {
			if (null == _spriteRenderer) {
				_spriteRenderer = GetComponent<SpriteRenderer> ();
			}
			return _spriteRenderer;
		}
	}
	private SpriteRenderer _spriteRenderer;
	public delegate void AnimationEventDelegate();
	public Dictionary<string, AnimationEventDelegate> animationEvents = new Dictionary<string, AnimationEventDelegate> ();
	public delegate void OnComplete(Animator animator);
	public OnComplete onComplete;

	public void AnimationEvent(string evt)
	{
		if (animationEvents.ContainsKey (evt)) {
			animationEvents [evt] ();
		}
	}

    public void ChangeAnimationClip(string name, AnimationClip clip)
    {
        AnimatorOverrideController overrideController = new AnimatorOverrideController();
        overrideController.runtimeAnimatorController = animator.runtimeAnimatorController;
        overrideController[name] = clip;
        animator.runtimeAnimatorController = overrideController;
    }
		
	void Update()
	{
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo (0);
		if (state.normalizedTime >= 1.0f && null != onComplete)
		{
			onComplete (animator);
		}

		spriteRenderer.sortingOrder = (int)(transform.position.y * -1000);
	}
}