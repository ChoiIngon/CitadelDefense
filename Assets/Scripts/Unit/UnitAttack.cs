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
		public Effect 	effect;
	}

	[System.Serializable]
	public class AttackData {
		public float 	range;
		public float 	power;
		public float 	speed;
		public float 	cooltime;
		public float	mana;
		public float	time;
	}

	public AttackInfo info;
	public AttackData data;
	public abstract void Attack ();
}
