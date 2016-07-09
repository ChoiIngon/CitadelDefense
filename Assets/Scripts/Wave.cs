using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave {
	public float remainTime = 0.0f;

	public IEnumerator WaveStart()
	{
		List<EnemyManager.EnemyFormation> formations = new List<EnemyManager.EnemyFormation> ();
		foreach(EnemyManager.EnemyFormation formation in GameManager.Instance.enemyManager.formations)
		{
			if (formation.firstWave > GameManager.Instance.waveLevel) {
				continue;
			}
			formations.Add (formation);
		}
		Vector3 spawnPosition = GameManager.Instance.enemyManager.transform.position;
		remainTime = GameManager.WAVE_TIME;

		while (0.0f < remainTime) {
			yield return new WaitForSeconds(Random.Range (1.0f, 1.5f));
			EnemyManager.EnemyFormation formation = formations [Random.Range (0, formations.Count)];
			foreach (Vector3 position in formation.positions) {
				EnemyUnit unitEnemy = (EnemyUnit)GameObject.Instantiate<EnemyUnit> (formation.enemy);
				unitEnemy.Init();
				unitEnemy.transform.position = spawnPosition + position;
				unitEnemy.transform.SetParent (GameManager.Instance.enemyManager.transform);
			}
		}

		while(0 < GameManager.Instance.enemyManager.transform.childCount) 
		{
			yield return null;
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
