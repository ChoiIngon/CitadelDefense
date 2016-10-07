using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave {
	public float remainTime = 0.0f;

	public virtual IEnumerator WaveStart()
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
			GameManager.Instance.uiWaveProgress.progress = remainTime / GameManager.WAVE_TIME;
			float waitTime = Random.Range (1.5f, 2.0f);
			remainTime -= waitTime;
			yield return new WaitForSeconds(waitTime);
			EnemyManager.SpawnInfo info = infos [Random.Range (0, infos.Count)];
			for(int i=0; i<info.count; i++) {
				EnemyUnit unitEnemy = (EnemyUnit)GameObject.Instantiate<EnemyUnit> (info.enemy);
				unitEnemy.transform.position = new Vector3 (spawnPosition.x + Random.Range (-1.0f, 1.0f), spawnPosition.y + Random.Range (-1.0f, 1.0f), 0.0f);
				unitEnemy.transform.SetParent (GameManager.Instance.enemyManager.transform);
			}
		}

		if (0 == GameManager.Instance.waveLevel % 5) {
			EnemyUnit boss = GameObject.Instantiate<EnemyUnit>(GameManager.Instance.enemyManager.boss [Random.Range (0, GameManager.Instance.enemyManager.boss.Length - 1)]);
			boss.transform.position = GameManager.Instance.enemyManager.transform.position;
			boss.transform.SetParent (GameManager.Instance.enemyManager.transform);
		}

		while(0 < GameManager.Instance.citadel.health && 0 < GameManager.Instance.enemyManager.transform.childCount) 
		{
			yield return new WaitForSeconds(0.1f);
		}

		if (0 < GameManager.Instance.citadel.health) {
			GameManager.Instance.WaveEnd (GameManager.WaveResult.Win);
		} else {
			GameManager.Instance.WaveEnd (GameManager.WaveResult.Lose);
		}
	}
}
