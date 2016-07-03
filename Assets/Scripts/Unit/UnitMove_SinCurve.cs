using UnityEngine;
using System.Collections;

public class UnitMove_SinCurve : UnitMove {
    private float height;
	public override void Init(Vector3 start, Vector3 end)
	{
		base.Init (start, end);
		height = distance / 4;
	}
	void Update() {
		if (1.0f < _interpolate) {
			enabled = false;
			return;
		}
		_interpolate += Time.deltaTime * speed / distance;
		Vector3 curPos = Vector3.Lerp (start, end, interpolate);
		curPos.y += Mathf.Sin (Mathf.PI * _interpolate) * height;
        float degree = 90.0f * Mathf.Cos(Mathf.PI * _interpolate) / Mathf.Max(height, 2.0f);
        transform.rotation = Quaternion.Euler(0, 0, degree);
		transform.position = curPos;
	}
}
