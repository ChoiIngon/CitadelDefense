using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
	public UnitEnemy [] enemys;
	private float deltaTime;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (GameManager.GameState.Play != GameManager.Instance.state) {
			return;
		}
		if (1.0f <= deltaTime) {
            UnitEnemy unitEnemy = (UnitEnemy)GameObject.Instantiate<UnitEnemy> (enemys[Random.Range(0, enemys.Length)]);
			unitEnemy.transform.position = new Vector3 (15.0f, Random.Range (3.0f, 6.0f), 0.0f);
			deltaTime = 0.0f;
			unitEnemy.transform.SetParent (transform);
		}
		deltaTime += Time.deltaTime;
	}
}
