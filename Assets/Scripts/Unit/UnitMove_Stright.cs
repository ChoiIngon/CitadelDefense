using UnityEngine;
using System.Collections;

public class UnitMove_Stright : UnitMove {
	public float height;
	public bool useRotation;
	public GameObject shadow;
	public override void Init(Vector3 start, Vector3 end)
	{
		base.Init (start, end);

		if (true == useRotation) {
			Vector3 from = end - start;
			float angle = Vector3.Angle (from, new Vector3 (from.x, 0.0f, 0.0f));
			if (0.0f > from.y) {
				angle = 360 - angle;
			}
			if (0.0f > from.x) {
				angle = 180 - angle;
			}
			transform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, angle));
		}
    }
	void Update() {
		if (1.0f < _interpolate) {
			enabled = false;
			return;
		}
		_interpolate += Time.deltaTime * speed / distance;
		if (null != buff) {
			_interpolate = buff (_interpolate);
		}
		Vector3 curPos = Vector3.Lerp (start, end, interpolate);
		transform.position = curPos;
		if (null != shadow) {
			curPos.y -= height * (1.0f - interpolate);
			shadow.transform.position = curPos;
			shadow.transform.rotation = Quaternion.Euler(0, 0, -transform.rotation.z);
		}
	}
}
