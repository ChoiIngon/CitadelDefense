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
		public string name;
		public Sprite icon;
		public string description;
	}

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
	private AttackData _data;
	public Buff buffPrefab;
	public UnitHit hitPrefab;

	[SerializeField]

	public delegate void BuffPower(ref float ret, float originalAttackPower);
	public delegate void BuffSpeed(ref float ret, float originalAttackSpeed);
	public delegate void BuffCooltime(ref float ret, float originalCooltime);
	public delegate void BuffMana(ref float ret, float orignaMana);

	public BuffPower powerBuff;
	public BuffSpeed speedBuff;
	public BuffCooltime cooltimeBuff;
	public BuffMana manaBuff;
	public abstract void Attack ();

	public virtual void Hit(Vector3 position)
	{
		if(null != hitPrefab)
		{
			UnitHit hit = GameObject.Instantiate<UnitHit>(hitPrefab);
			hit.attack = this;
			hit.transform.position = position;
		}
	}

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
		_data.power = GetUpgradedValue(level, init.power, upgrade.power, max.power);
		_data.minRange = GetUpgradedValue (level, init.minRange, upgrade.minRange, max.minRange);
		_data.maxRange = GetUpgradedValue (level, init.maxRange, upgrade.maxRange, max.maxRange);
		_data.speed = GetUpgradedValue(level, init.speed, upgrade.speed, max.speed);
		_data.cooltime = GetUpgradedValue(level, init.cooltime, upgrade.cooltime, max.cooltime);
		_data.mana = GetUpgradedValue(level, init.mana, upgrade.mana, max.mana);
		_data.time = GetUpgradedValue(level, init.time, upgrade.time, max.time);
    }

	private float GetUpgradedValue(int level, float init, float upgrade, float max)
	{
		float v = init + upgrade * (level - 1);
		v = Mathf.Min (v, max);
		v = Mathf.Max (v, 0.0f);
		return v;
	}
}
