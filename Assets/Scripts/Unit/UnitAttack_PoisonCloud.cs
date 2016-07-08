using UnityEngine;
using System.Collections;

public class UnitAttack_PoisonCloud : UnitAttack {
    public GameObject poisonCloudPrefab;
	public Vector3 initPosition;

	public TouchEvent unitTouchEvent;
	
	public override void Attack()
	{
        GameObject obj = GameObject.Instantiate<GameObject>(poisonCloudPrefab);
        obj.transform.position = initPosition;
        UnitColliderDot attack = obj.GetComponent<UnitColliderDot>();
        attack.power = data.power;
        unitTouchEvent.gameObject.SetActive(true);
    }
}
