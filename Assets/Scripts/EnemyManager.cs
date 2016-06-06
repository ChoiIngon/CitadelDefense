using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
	public UnitEnemy enemy;
	public UnitCitadel citadel;
	private float deltaTime;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (1.0f <= deltaTime) {
			UnitEnemy unitEnemy = (UnitEnemy)GameObject.Instantiate<UnitEnemy> (enemy);
			unitEnemy.citadel = citadel;
			unitEnemy.transform.position = new Vector3 (15.0f, Random.Range (-3.0f, 3.0f), 0.0f);
			deltaTime = 0.0f;

			unitEnemy.transform.SetParent (transform);
		}
		deltaTime += Time.deltaTime;
	}
}
