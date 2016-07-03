using UnityEngine;
using System.Collections;


public abstract class UnitAttack : MonoBehaviour {
	[HideInInspector]
	public Unit 	self;
	[HideInInspector]
	public Unit 	target;

	[System.Serializable]
	public class AttackData {
		public float 	range;
		public float 	power;
		public float 	speed;
		public float 	cooltime;
		public float	mana;
		//public float	time;
	}

    [System.Serializable]
    public class AttackInfo : AttackData
    {
        public string name;
        public string description;
        public Sprite icon;
        //public Effect effect;
    }

    public AttackInfo info;
	public AttackData data;
	public AttackData max;
	public AttackData upgrade;
	public abstract void Attack ();
}
