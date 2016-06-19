using UnityEngine;
using System.Collections;


public abstract class UnitAttack : MonoBehaviour {
	[HideInInspector]
	public Unit 	self;
	[HideInInspector]
	public Unit 	target;

	[System.Serializable]
	public class AttackInfo
	{
		public string 	name;
		public string 	description;
		public Sprite 	icon;
		public float 	range;
		public int 		power;
		public float 	speed;
		public float 	cooltime;
		public float	mana;
		public float	time;
		public Effect 	effect;
	}

	[HideInInspector]
	public AttackInfo info;
	public abstract void Attack ();
}
