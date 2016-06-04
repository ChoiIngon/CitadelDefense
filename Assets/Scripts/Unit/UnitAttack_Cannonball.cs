using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UnitAttack_Cannonball : UnitAttack {
	public GameObject cannonball;
	[HideInInspector]
	public List<UnitMove_SinCurve> unitMoves = new List<UnitMove_SinCurve>();

	public override void Attack()
	{
		float distance = Vector3.Distance (start, target);

		GameObject go = GameObject.Instantiate<GameObject> (cannonball);
		UnitMove_SinCurve unitMove = go.GetComponent<UnitMove_SinCurve> ();
		unitMove.Init (start, target, distance / 4, 9.0f);

		unitMoves.Add (unitMove);
	}

	void Update() {
		List<UnitMove_SinCurve> completeMove = new List<UnitMove_SinCurve> ();
		foreach (UnitMove_SinCurve unitMove in unitMoves) {
			if (1.0f <= unitMove.interpolate) {
				completeMove.Add (unitMove);
				Effect go = GameObject.Instantiate<Effect> (effect);
				go.transform.position = unitMove.end;
			}
		}

		foreach (UnitMove_SinCurve unitMove in completeMove) {
			unitMoves.Remove (unitMove);
			DestroyImmediate (unitMove.gameObject, true);
		}
	}
}
