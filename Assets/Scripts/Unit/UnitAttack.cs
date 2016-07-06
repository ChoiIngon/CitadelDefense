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
    public virtual void Upgrade(int level)
    {
        if(1 > level)
        {
            throw new System.Exception("invalid level");
        }
        data.power = info.power + (info.power * upgrade.power * (level - 1));
        data.range = info.range + (info.range * upgrade.range * (level - 1));
        data.speed = info.speed + (info.speed * upgrade.speed * (level - 1));
    }
}
