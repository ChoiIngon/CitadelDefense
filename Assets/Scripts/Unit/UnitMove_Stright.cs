using UnityEngine;
using System.Collections;

public class UnitMove_Stright : UnitMove {
	public override void Init(Vector3 start, Vector3 end)
	{
		base.Init (start, end);

        Vector3 from = end - start;
        float angle = Vector3.Angle(from, new Vector3(from.x, 0.0f, 0.0f));
        if (0.0f > from.y)
        {
            angle = 360 - angle;
        }
        if (0.0f > from.x)
        {
            angle = 180 - angle;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, angle));
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
