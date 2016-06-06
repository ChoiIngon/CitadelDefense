using UnityEngine;
using System.Collections;


public abstract class UnitAttack : MonoBehaviour {
	public Vector3 	target;
	public Vector3 	start;
	public Effect 	effect;

	public float 	range;
	public int 		power;
	public float 	speed;
	public float 	interval;
	public abstract void Attack ();
}
