using UnityEngine;
using System.Collections;

public class UnitHit : MonoBehaviour {
	public UnitAttack attack;
	public virtual void Init(Vector3 position, float power)
	{
		GetComponent<UnitColliderAttack> ().attack = attack;
		transform.position = position;
	}
}
