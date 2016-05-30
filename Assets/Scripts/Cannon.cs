using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {
	public GameObject cannonball;
	public float attackSpeed;
	public float elapsedTime;
	// Use this for initialization
	void Start () {
		elapsedTime = float.MaxValue;
	}
	
	// Update is called once per frame
	void Update () {
		if (elapsedTime >= attackSpeed) {
			
			GameObject go = GameObject.Instantiate (cannonball);
			go.transform.position = transform.position;
			Attack_RangeCurve attack = go.GetComponent<Attack_RangeCurve> ();
			attack.startPosition = transform.localPosition;
			attack.targetPosition = attack.startPosition;
			attack.targetPosition.x += 15;
			elapsedTime = 0.0f;
		} else {
			elapsedTime += Time.deltaTime;
		}
	}
}
