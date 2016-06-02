using UnityEngine;
using System.Collections;

public class MovementSinCurve : MonoBehaviour {

	public float speed;
	public Vector3 start;
	public Vector3 end;
	public float height;
	private float interpolate;
	private float distance;

	public void Init(Vector3 start, Vector3 end)
	{
		this.start = start;
		this.end = end;
	}

	void Start()
	{
		distance = Vector3.Distance (start, end);
	}

	void Update() {
		interpolate += Time.deltaTime * speed / distance;
		Vector3 curPos = Vector3.Lerp (start, end, interpolate);
		curPos.y += Mathf.Sin (Mathf.PI * interpolate) * height;
		transform.position = curPos;
		if (1.0f <= interpolate) {
			Destroy(gameObject);
		}
	}
}
