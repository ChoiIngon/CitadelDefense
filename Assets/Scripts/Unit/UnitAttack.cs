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

	[System.Serializable]
	public class Info
	{
		public string 	name;
		public string 	description;
		public Sprite 	sprite;
		public float 	range;
		public int 		power;
		public float 	speed;
		public float 	interval;
		public float	mana;
		public float	time;
		public Effect 	effect;
	}
	public Info info;
	public abstract void Attack ();
}
