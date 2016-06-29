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
	public int remainTime;
	void Start()
	{
		Transform child = transform.FindChild ("SpawnPoint");
		spawnPoint = child.position;
	}
	void OnEnable()
	{
		remainTime = GameManager.WAVE_TIME;
	}
	// Update is called once per frame
	void Update () {
		if (1.0f <= deltaTime) {
			remainTime -= 1;
			if (0 == remainTime) {
				GameManager.Instance.WaveEnd (GameManager.WaveResult.Win);
				return;
			}
			if (0 == formations.Length) {
				return;
			}
			for(int i=0; i<formations.Length; i++)
			{
				int index = Random.Range (0, formations.Length);
				EnemyFormation formation = formations [index];
				if (formation.firstWave > GameManager.Instance.wave) {
					return;
				}
				foreach (Vector3 position in formation.positions) {
					EnemyUnit unitEnemy = (EnemyUnit)GameObject.Instantiate<EnemyUnit> (formation.enemy);
                    unitEnemy.Init();
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
		while (1 < transform.childCount) {
			EnemyUnit unitEnemy = transform.GetChild (transform.childCount - 1).GetComponent<EnemyUnit> ();
			if (null != unitEnemy) {
				unitEnemy.transform.SetParent (null);
				DestroyObject (unitEnemy.gameObject);
			}
		}
	}
}
