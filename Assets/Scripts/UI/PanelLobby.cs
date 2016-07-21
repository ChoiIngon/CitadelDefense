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
		buttonUpgradeCitadel.onClick.AddListener (GameManager.Instance.citadel.Upgrade);
		buttonBuff.onClick.AddListener (() => {
			GameManager.Instance.uiCitadelBuffPanel.gameObject.SetActive(true);
		});
		buttonItem.onClick.AddListener (() => {
			GameManager.Instance.uiItemPanel.gameObject.SetActive(true);
		});
	}
}