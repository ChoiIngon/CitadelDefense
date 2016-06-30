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
