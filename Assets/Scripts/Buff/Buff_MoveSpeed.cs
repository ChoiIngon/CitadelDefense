using UnityEngine;
using System.Collections;

public class Buff_MoveSpeed : Buff {
	private UnitMove move;
	public float time;
	public float value;
	private float deltaTime;
	// Use this for initialization
	void Start () {
		deltaTime = 0.0f;
		move = GetComponentInParent<UnitMove> ();
		if (null == move) {
			return;
		}
		if (null != move.buff) {
			return;
		}
		move.buff += SpeedDown;
	}

	float SpeedDown(float interpolate)
	{
		return interpolate * value;
	}
	// Update is called once per frame
	void Update () {
		if (null != move) {
			deltaTime += Time.deltaTime;
			if (deltaTime >= time) {
				Destroy (gameObject);
				move.buff = null;
			}		
		}
	}
}
