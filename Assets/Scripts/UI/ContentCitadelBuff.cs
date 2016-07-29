using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContentCitadelBuff : MonoBehaviour {
	[HideInInspector]
	public CitadelBuff buff;
	public PanelCitadelBuff panel;
	public Image icon;
	public Text level;
	// Use this for initialization
	void Start () {
        Button button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);

		icon.sprite = buff.info.icon;
		level.text = buff.level.ToString ();
	}

	public void OnClick()
	{
		panel.selectedBuff = this;
		panel.description.text = string.Format(buff.info.description, Mathf.Abs(buff.value) * 100.0f);
		Text text = panel.levelup.transform.FindChild("Text").GetComponent<Text>();
		if (buff.maxLevel == buff.level) {
			text.text = "Max Level";
		} else {
			text.text = "Level up\r\n(" + (buff.upgradeCost * (buff.level + 1)).ToString () + " G)";
		}
	}

	public void Upgrade()
	{
		buff.Upgrade ();
		level.text = buff.level.ToString ();
		panel.description.text = string.Format(buff.info.description, Mathf.Abs(buff.value) * 100.0f);
		Text text = panel.levelup.transform.FindChild("Text").GetComponent<Text>();
		if (buff.maxLevel == buff.level) {
			text.text = "Max Level";
		} else {
			text.text = "Level up\r\n(" + (buff.upgradeCost * (buff.level + 1)).ToString () + " G)";
		}
	}
}
