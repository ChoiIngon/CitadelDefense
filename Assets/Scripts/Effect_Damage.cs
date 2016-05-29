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
}
