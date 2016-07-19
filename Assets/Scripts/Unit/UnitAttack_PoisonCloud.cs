using UnityEngine;
using System.Collections;

public class UnitAttack_PoisonCloud : UnitAttack {
    public GameObject poisonCloudPrefab;
	
	public Vector3 initPosition;
	private float deltaTime;
	public override void Attack()
	{
		gameObject.SetActive (true);
		deltaTime = 0.0f;

        GameObject obj = GameObject.Instantiate<GameObject>(poisonCloudPrefab);
        obj.transform.position = initPosition;
		transform.position = initPosition;
    }

	void Update()
	{
		deltaTime += Time.deltaTime;
		if (deltaTime >= 1.0f) {
			gameObject.SetActive (false);
		}
	}
	void OnTriggerEnter2D(Collider2D col) {
		if (targetTag == col.gameObject.tag) {
			UnitColliderDamage colliderDamage = col.gameObject.GetComponent<UnitColliderDamage> ();
			if (null != colliderDamage) {
				Buff_Poison buff = (Buff_Poison)GameObject.Instantiate<Buff>(buffPrefab);
                buff.unit = colliderDamage.unit;
				buff.time = data.time;
				buff.damage = data.power;
				buff.interval = 1.0f / data.speed;
				buff.transform.SetParent (colliderDamage.unit.transform);
			}
		}
	}
}
