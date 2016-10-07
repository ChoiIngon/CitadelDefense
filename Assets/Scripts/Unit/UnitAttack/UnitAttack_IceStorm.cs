using UnityEngine;
using System.Collections;

public class UnitAttack_IceStorm : UnitAttack {
	float deltaTime;
	public SpriteRenderer background;

	public override void Attack()
	{
		transform.position = Vector3.zero;
		gameObject.SetActive (true);
		deltaTime = 0.0f;
		StartCoroutine (Damage ());
    }

	IEnumerator Damage()
	{
		while (deltaTime < data.time) {
			for (int i = 0; i < GameManager.Instance.enemyManager.transform.childCount; i++) {
				EnemyUnit enemy = GameManager.Instance.enemyManager.transform.GetChild (i).GetComponent<EnemyUnit> ();
				Damage (enemy);
			}
			yield return new WaitForSeconds (0.5f);
		}
	}
	void Update()
	{
		deltaTime += Time.deltaTime;
		if (deltaTime >= data.time) {
			gameObject.SetActive (false);
		}
	}
}
