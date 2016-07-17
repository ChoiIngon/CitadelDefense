using UnityEngine;
using System.Collections;

public class UnitAttack_IceStorm : UnitAttack {
	public float time;
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
		while (deltaTime < time) {
			for (int i = 0; i < GameManager.Instance.enemyManager.transform.childCount; i++) {
				EnemyUnit enemy = GameManager.Instance.enemyManager.transform.GetChild (i).GetComponent<EnemyUnit> ();
				enemy.Damage ((int)data.power);
			}
			yield return new WaitForSeconds (0.5f);
		}
	}
	void Update()
	{
		deltaTime += Time.deltaTime;
		for(int i=0; i< GameManager.Instance.enemyManager.transform.childCount; i++)
		{
			EnemyUnit enemy = GameManager.Instance.enemyManager.transform.GetChild (i).GetComponent<EnemyUnit> ();
			enemy.direction.x = 0.0f;
			enemy.unitAnimation.animator.speed = 0.0f;
			enemy.unitAnimation.spriteRenderer.color = Color.blue; 
		}
		if (deltaTime >= time) {
			for(int i=0; i< GameManager.Instance.enemyManager.transform.childCount; i++)
			{
				EnemyUnit enemy = GameManager.Instance.enemyManager.transform.GetChild (i).GetComponent<EnemyUnit> ();
				enemy.direction.x = -1.0f;
				enemy.unitAnimation.animator.speed = 1.0f;
				enemy.unitAnimation.spriteRenderer.color = Color.white; 
			}
			gameObject.SetActive (false);
		}
	}
}
