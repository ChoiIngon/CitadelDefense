using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ContentSkillInfo : MonoBehaviour {
	public Image icon;
	public Text skillName;
	public Text mana;
	public Text cooltime;
	public Text description;
	public void Init(UnitAttack attack)
	{
		if (null == attack) {
			return;
		}
		icon.sprite = attack.info.icon;
		skillName.text = attack.info.name;
		mana.text = attack.data.mana.ToString();
		cooltime.text = attack.data.cooltime.ToString ();
		description.text = attack.info.description;
	}
}
