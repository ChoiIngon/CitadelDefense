using UnityEngine;
using System.Collections;

public class UnitMove_Position : UnitMove {
	// Use this for initialization
	public override void Init(Vector3 start, Vector3 end)
	{
		base.Init (start, end);
		_interpolate = 1.0f;
		transform.position = end;
	}
}
