using UnityEngine;
using System.Collections;


public abstract class UnitAttack : MonoBehaviour {
	[HideInInspector]
	public Unit 	self;
	[HideInInspector]
	public Unit 	target;

	[System.Serializable]
	public class AttackData {
		public float 	minRange;
		public float	maxRange;
		public float 	power;
		public float 	speed;
		public float 	cooltime;
		public float	mana;
		//public float	time;
	}

    [System.Serializable]
    public class AttackInfo
    {
        public string name;
		public Sprite icon;
        public string description;
    }

    public AttackInfo info;

	public AttackData init;
	public AttackData max;
	public AttackData upgrade;
	[ReadOnly] public AttackData data;
	public abstract void Attack ();
	public virtual void Upgrade(int level)
    {
        if(1 > level)
        {
            throw new System.Exception("invalid level");
        }
		data.power = init.power + upgrade.power * (level-1);
		data.maxRange = init.maxRange + upgrade.maxRange * (level-1);
		data.speed = init.speed + upgrade.speed * (level-1);
		data.cooltime = init.cooltime + upgrade.cooltime * (level - 1);
		data.mana = init.mana + upgrade.mana * (level - 1);
    }
}
