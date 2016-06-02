using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MovementSinCurve movement = gameObject.AddComponent<MovementSinCurve> ();
		movement.start = new Vector3 (-10, 0, 0);
		movement.end = new Vector3 (10, 0, 0);
		movement.speed = 5;
		movement.height = 3;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
