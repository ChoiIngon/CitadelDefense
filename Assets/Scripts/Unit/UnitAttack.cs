using UnityEngine;
using System.Collections;


public abstract class UnitAttack : MonoBehaviour {
	public string targetTag;
	public Buff buffPrefab;
	[HideInInspector]
	public Unit 	self;
	[HideInInspector]
	public Unit 	target;

	[System.Serializable]
	public struct AttackData {
		public float 	minRange;
		public float	maxRange;
		public float 	power;
		public float 	speed;
		public float 	cooltime;
		public float	mana;
		public float	time;
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
	public AttackData data {
		get {
			AttackData tmp = _data;
			if (null != powerBuff) {
				powerBuff (ref tmp.power, _data.power);
			}

			if (null != speedBuff) {
				speedBuff (ref tmp.speed, _data.speed);
			}
			if (null != cooltimeBuff) {
				cooltimeBuff (ref tmp.cooltime, _data.cooltime);
				//tmp.cooltime = Mathf.Max (10.0f, tmp.cooltime);
			}

			if (null != manaBuff) {
				manaBuff (ref tmp.mana, _data.mana);
			}

			return tmp;
		}
	}
	[SerializeField]
	private AttackData _data;
	public delegate void BuffPower(ref float ret, float originalAttackPower);
	public delegate void BuffSpeed(ref float ret, float originalAttackSpeed);
	public delegate void BuffCooltime(ref float ret, float originalCooltime);
	public delegate void BuffMana(ref float ret, float orignaMana);

	public BuffPower powerBuff;
	public BuffSpeed speedBuff;
	public BuffCooltime cooltimeBuff;
	public BuffMana manaBuff;
	public abstract void Attack ();
	public virtual void Damage(Unit unit)
	{
		unit.Damage ((int)data.power);
		if (null != buffPrefab) {
			Buff buff = GameObject.Instantiate<Buff> (buffPrefab);
            buff.unit = unit;
			buff.transform.SetParent (unit.transform);
		}
	}
	public virtual void Upgrade(int level)
    {
        if(1 > level)
        {
            throw new System.Exception("invalid level");
        }
		_data.power = init.power + upgrade.power * (level-1);
		_data.minRange = init.minRange + upgrade.minRange * (level-1);
		_data.maxRange = init.maxRange + upgrade.maxRange * (level-1);
		_data.speed = init.speed + upgrade.speed * (level-1);
		_data.cooltime = init.cooltime + upgrade.cooltime * (level - 1);
		_data.mana = init.mana + upgrade.mana * (level - 1);
		_data.time = init.time + upgrade.time * (level - 1);
    }
}
