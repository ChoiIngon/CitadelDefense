using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
	[System.Serializable]
	public class EnemyFormation
	{
		public EnemyUnit enemy;
		public int firstWave;
		public Vector3[] positions;
	}
	public EnemyFormation [] formations;
	private Vector3 spawnPoint;
	private float deltaTime;
	void Start()
	{
		Transform child = transform.FindChild ("SpawnPoint");
		spawnPoint = child.position;
	}
	// Update is called once per frame
	void Update () {
		if (GameManager.GameState.Play != GameManager.Instance.state) {
			return;
		}
		if (1.0f <= deltaTime) {
			if (0 == formations.Length) {
				return;
			}
			for(int i=0; i<formations.Length; i++)
			{
				int index = Random.Range (0, formations.Length);
				EnemyFormation formation = formations [index];
				if (formation.firstWave > GameManager.Instance.wave) {
					continue;
				}
				foreach (Vector3 position in formation.positions) {
					EnemyUnit unitEnemy = (EnemyUnit)GameObject.Instantiate<EnemyUnit> (formation.enemy);
					unitEnemy.transform.position = spawnPoint + position;
					unitEnemy.transform.SetParent (transform);
				}
				break;
			}
			deltaTime = 0.0f;
		}
		deltaTime += Time.deltaTime;
	}

	public void Clear()
	{
	}
}
