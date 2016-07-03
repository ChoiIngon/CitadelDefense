using UnityEngine;
using System.Collections;

public class UnitHit : MonoBehaviour {
	protected float power;
	public virtual void Init(Vector3 position, float power)
	{
		transform.position = position;
		this.power = power;
	}
}
