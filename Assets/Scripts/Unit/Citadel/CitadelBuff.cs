using UnityEngine;
using System.Collections;

public class CitadelBuff : MonoBehaviour {
	[System.Serializable]
	public class Info
	{
		public string name;
		public string description;
		public Sprite icon;
	}
	public Info info;
	public int level;
	public int maxLevel;
	public int upgradeCost;
	public float value;
	public virtual void Init()
	{
	}
	public virtual void Upgrade()
	{
	}
	public void Buff(ref float ret, float originalValue)
	{
		ret += originalValue * value;
	}
}
