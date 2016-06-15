using UnityEngine;
using System.Collections;


public abstract class UnitAttack : MonoBehaviour {
	public Vector3 	target;
	public Vector3 	start;
	public Effect 	effect;

	[System.Serializable]
	public class AttackInfo
	{
		public string 	name;
		public string 	description;
		public Sprite 	sprite;
		public float 	range;
		public int 		power;
		public float 	speed;
		public float 	cooltime;
		public float	mana;
		public float	time;
		public Effect 	effect;
	}

	public AttackInfo info;
	public abstract void Attack ();
}
