using UnityEngine;
using System.Collections;

public class UnitMove_Stright : MonoBehaviour {
	public float speed;
	public Vector3 start;
	public Vector3 end;
	public float interpolate {
		get { return _interpolate; }
	}
	[SerializeField]
	private float _interpolate;
	private float distance = 0.0f;

	public void Init(Vector3 start, Vector3 end, float speed)
	{
		_interpolate = 0.0f;
		transform.position = start;
		this.start = start;
		this.end = end;
		this.speed = speed;
		distance = Vector3.Distance (start, end);
	}

	void Update() {
		if (1.0f < _interpolate) {
			return;
		}
		_interpolate += Time.deltaTime * speed / distance;
		transform.position = Vector3.Lerp (start, end, interpolate);
	}
}
