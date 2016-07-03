using UnityEngine;
using System.Collections;

public class UnitMove_Stright : UnitMove {
	public override void Init(Vector3 start, Vector3 end)
	{
		base.Init (start, end);
	}
	void Update() {
		if (1.0f < _interpolate) {
			enabled = false;
			return;
		}
		_interpolate += Time.deltaTime * speed / distance;
		transform.position = Vector3.Lerp (start, end, interpolate);
	}
}
