using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
	[System.Serializable]
	public class SpawnInfo
	{
		public EnemyUnit enemy;
		public int firstWave;
		public Rect rect;
		public int count;
	}
	public SpawnInfo [] spwan;
	public EnemyUnit[] boss;

	public void Clear()
	{
		while (0 < transform.childCount) {
			EnemyUnit unitEnemy = transform.GetChild (0).GetComponent<EnemyUnit> ();
			if (null != unitEnemy) {
				unitEnemy.transform.SetParent (null);
				DestroyObject (unitEnemy.gameObject);
			}
		}
	}
}
