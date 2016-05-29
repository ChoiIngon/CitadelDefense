using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
[RequireComponent (typeof (Animator))]
public class Effect : MonoBehaviour {
	Animator animator;
	public string stateName;
	public string animationControllerPath;
	// Use this for initialization
	protected void Start () {
		gameObject.layer = LayerMask.NameToLayer ("Effect");
		animator = GetComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> (animationControllerPath);
		GetComponent<Renderer> ().sortingLayerName = "Effect";
		GetComponent<Renderer> ().sortingOrder = 0;
	}

	protected void Update () {
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo (0);
		if (state.IsName(stateName) && state.normalizedTime >= 1.0f)
		{
			DestroyImmediate (gameObject, true);
		}
	}
}
