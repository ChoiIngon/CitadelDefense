using UnityEngine;
using System.Collections;

public class Buff_Freeze : MonoBehaviour {
	public Unit target;
	public float time;
	float deltaTime;
	float speed;
	// Use this for initialization
	void Start () {
		deltaTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		deltaTime += Time.deltaTime;
		if (deltaTime >= time) {
			Destroy (gameObject);
		}
	}
}
