﻿using UnityEngine;
using System.Collections;

public class UnitMove_SinCurve : UnitMove {
    private float height;
	public GameObject shadow;
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
		if (null != shadow) {
			shadow.transform.position = curPos;
		}
		curPos.y += Mathf.Sin (Mathf.PI * _interpolate) * height;
		transform.position = curPos;

		float degree = 90.0f * Mathf.Cos(Mathf.PI * _interpolate) / Mathf.Max(height, 2.0f);
		transform.rotation = Quaternion.Euler(0, 0, degree);
		if (null != shadow) {
			shadow.transform.rotation = Quaternion.Euler(0, 0, -transform.rotation.z);
		}
	}
}
