using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelCitadel : MonoBehaviour {
    public Button waveStart;
	public Button buttonUpgrade;
	public Button buttonBuff;
	public Button buttonItem;
	public Button buttonExit;
	// Use this for initialization
	void Start () {
        waveStart.onClick.AddListener(GameManager.Instance.WaveStart);
		buttonUpgrade.onClick.AddListener (() => {
			GameManager.Instance.citadel.Upgrade();
			buttonUpgrade.transform.FindChild ("Text").GetComponent<Text> ().text = 
				"요새 업그레이드" + "\r\n" +
				"<size=14>(" + (GameManager.Instance.citadel.upgradeCost * GameManager.Instance.citadel.level).ToString() + " G)</size>";
		});
		buttonBuff.onClick.AddListener (() => {
			GameManager.Instance.uiCitadelBuffPanel.gameObject.SetActive(true);
		});
		buttonExit.onClick.AddListener(() => {
			GameManager.Instance.Quit();
		});
		/*
		buttonItem.onClick.AddListener (() => {
			GameManager.Instance.uiItemPanel.gameObject.SetActive(true);
		});
		*/
	}

	public void OnEnable() {
		buttonUpgrade.transform.FindChild ("Text").GetComponent<Text> ().text = 
			"요새 업그레이드" + "\r\n" +
			"<size=14>(" + (GameManager.Instance.citadel.upgradeCost * GameManager.Instance.citadel.level).ToString() + " G)</size>";
	}
}