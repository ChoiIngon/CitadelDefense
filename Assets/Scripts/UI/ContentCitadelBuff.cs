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
        button.onClick.AddListener(() =>
        {
			panel.selectedBuff = buff;
			panel.description.text = buff.info.description;
        });

		icon.sprite = buff.info.icon;
		level.text = buff.level.ToString ();
	}
}
