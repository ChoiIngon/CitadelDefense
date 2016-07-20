using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave {
	public float remainTime = 0.0f;

	public IEnumerator WaveStart()
	{
		List<EnemyManager.SpawnInfo> infos = new List<EnemyManager.SpawnInfo> ();
		foreach(EnemyManager.SpawnInfo info in GameManager.Instance.enemyManager.spwan)
		{
			if (info.firstWave > GameManager.Instance.waveLevel) {
				continue;
			}
			infos.Add (info);
		}
		Vector3 spawnPosition = GameManager.Instance.enemyManager.transform.position;

		remainTime = GameManager.WAVE_TIME;

		while (0.0f < remainTime) {
			yield return new WaitForSeconds(Random.Range (1.0f, 1.5f));
			spawnPosition.y = Random.Range (2.5f, 3.5f);
			EnemyManager.SpawnInfo info = infos [Random.Range (0, infos.Count)];
			for(int i=0; i<info.count; i++) {
				EnemyUnit unitEnemy = (EnemyUnit)GameObject.Instantiate<EnemyUnit> (info.enemy);
				unitEnemy.transform.position = new Vector3 (spawnPosition.x + Random.Range (-1.0f, 1.0f), spawnPosition.y + Random.Range (-1.0f, 1.0f), 0.0f);
				unitEnemy.transform.SetParent (GameManager.Instance.enemyManager.transform);
			}
		}

		while(0 < GameManager.Instance.enemyManager.transform.childCount) 
		{
			yield return null;
		}

		while (0 < GameManager.Instance.creatures.childCount) {
			Transform child = GameManager.Instance.creatures.GetChild (0);
			child.SetParent (null);
			Object.Destroy (child.gameObject);
		}

		if (0 < GameManager.Instance.citadel.hp) {
			GameManager.Instance.WaveEnd (GameManager.WaveResult.Win);
		} else {
			GameManager.Instance.WaveEnd (GameManager.WaveResult.Lose);
		}
	}

	public void Update()
	{
		remainTime -= Time.deltaTime;
	}
}
