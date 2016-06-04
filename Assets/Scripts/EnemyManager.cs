using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
	public Enemy enemy;
	public UnitCitadel citadel;
	private float deltaTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (1.0f <= deltaTime) {
			Enemy go = (Enemy)GameObject.Instantiate<Enemy> (enemy);
			go.citadel = citadel;
			go.transform.position = new Vector3 (15.0f, Random.Range (-3.0f, 3.0f), 0.0f);
			deltaTime = 0.0f;

			go.transform.SetParent (transform);
		}
		deltaTime += Time.deltaTime;
	}
}
