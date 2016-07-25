using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelLobby : MonoBehaviour {
    public Button advertisement;
    public Button waveStart;
	public Button buttonUpgradeCitadel;
	public Button buttonBuff;
	public Button buttonItem;
	// Use this for initialization
	void Start () {
        waveStart.onClick.AddListener(GameManager.Instance.WaveStart);
		buttonUpgradeCitadel.onClick.AddListener (() => {
			GameManager.Instance.citadel.Upgrade();
			buttonUpgradeCitadel.transform.FindChild ("Text").GetComponent<Text> ().text = 
				"요새 업그레이드" + "\r\n" +
				"(" + (GameManager.Instance.citadel.upgradeCost * GameManager.Instance.citadel.level).ToString() + " G)";
		});
		buttonBuff.onClick.AddListener (() => {
			GameManager.Instance.uiCitadelBuffPanel.gameObject.SetActive(true);
		});
		buttonItem.onClick.AddListener (() => {
			GameManager.Instance.uiItemPanel.gameObject.SetActive(true);
		});
	}

	public void OnEnable() {
		buttonUpgradeCitadel.transform.FindChild ("Text").GetComponent<Text> ().text = 
			"요새 업그레이드" + "\r\n" +
			"(" + (GameManager.Instance.citadel.upgradeCost * GameManager.Instance.citadel.level).ToString() + " G)";
	}
}