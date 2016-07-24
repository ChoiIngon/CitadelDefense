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
	[System.Serializable]
	public class SaveData {
		public int level;
	}
	public Info info;
	public int level;
	public int maxLevel;
	public int upgradeCost;
	public float upgradeValue;
	public float value {
		get {
			return upgradeValue * level;
		}
	}
	public virtual void Init()
	{
	}
	public virtual bool Upgrade()
	{
		if (level >= maxLevel) {
			GameManager.Instance.uiMessageBox.message = "최대 레벨에 도달했습니다";
			return false;
		}
		int cost = level * upgradeCost;
		if (GameManager.Instance.gold < cost) {
			GameManager.Instance.uiMessageBox.message = "골드가 부족 합니다";
			return false;
		}
		GameManager.Instance.gold -= cost;
		level++;
		Init ();
		return true;
	}

	public void Buff(ref float ret, float originalValue)
	{
		ret += originalValue * value;
	}
}
