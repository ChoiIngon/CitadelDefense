using UnityEngine;
using System.Collections;

public class Attack_RangeCurve : Attack {
	public float moveSpeed;
	public Vector3 startPosition;
	public Vector3 targetPosition;
	public GameObject effect;

	private float startTime;
	void Start() {
		base.Start ();
		startTime = Time.time;
	}

	void Update() {
		Vector3 center = (startPosition + targetPosition) * 0.5F;
		center -= new Vector3(0, 1, 0);
		Vector3 startRelCenter = startPosition - center;
		Vector3 tartgetRelCenter = targetPosition - center;
		float distance = Vector3.Distance (startPosition, targetPosition);
		float moveTime = distance/moveSpeed;
		float fracComplete = (Time.time - startTime) / moveTime;
		transform.position = Vector3.Slerp(startRelCenter, tartgetRelCenter, fracComplete);
		transform.position += center;

		if (1.0 <= fracComplete) {
			GameObject go = GameObject.Instantiate(effect);
			go.transform.position = targetPosition;
			DestroyImmediate (gameObject, true);
		}
	}
}
