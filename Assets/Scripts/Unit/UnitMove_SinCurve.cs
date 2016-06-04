using UnityEngine;
using System.Collections;

public class UnitMove_SinCurve : MonoBehaviour {
	public float speed;
	public Vector3 start;
	public Vector3 end;
	public float height;
	public float interpolate {
		get { return _interpolate; }
	}
	[SerializeField]
	private float _interpolate;
	private float distance = 4.0f;

	public void Init(Vector3 start, Vector3 end, float height, float speed)
	{
		_interpolate = 0.0f;
		transform.position = start;
		this.start = start;
		this.end = end;
		this.height = height;
		this.speed = speed;
		distance = Vector3.Distance (start, end);
	}
		
	void Update() {
		if (1.0f < _interpolate) {
			return;
		}
		_interpolate += Time.deltaTime * speed / distance;
		Vector3 curPos = Vector3.Lerp (start, end, interpolate);
		curPos.y += Mathf.Sin (Mathf.PI * _interpolate) * height;
		transform.position = curPos;
	}
}
