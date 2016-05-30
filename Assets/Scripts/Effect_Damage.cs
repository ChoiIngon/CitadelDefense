using UnityEngine;
using System.Collections;

[RequireComponent (typeof (TextMesh))]
public class Effect_Damage : Effect {
	void Start()
	{
		base.Start ();
		GetComponent<MeshRenderer> ().sortingLayerName = "Effect";
		GetComponent<MeshRenderer> ().sortingOrder = 0;
	}

	public void Init (int damage) {
		TextMesh text = GetComponent<TextMesh> ();
		text.text = damage.ToString ();
	}

	protected void Update () {
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo (0);
		if (state.IsName(stateName) && state.normalizedTime >= 1.0f)
		{
			DestroyImmediate (transform.parent.gameObject, true);
			//DestroyImmediate (gameObject, true);
		}
	}
}
