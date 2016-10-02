using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelCitadel : MonoBehaviour {
    public Button waveStart;
	public Button buttonUpgrade;
	public Button buttonBuff;
	public Button buttonItem;
	public Button buttonExit;
	public Text textTip;

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

	IEnumerator DisplayTips()
	{
		string[] tips = new string[] {
			"Tip: 전투중 마법사를 터치하면 특수 스킬이 발동 됩니다.",
			"Tip: 마법진 스킬은 마법진을 터치 할때 마다 보너스 공격이 나갑니다."
		};

		while (true) {
			for (int i = 0; i < tips.Length; i++) {
				textTip.text = tips [i];
				yield return new WaitForSeconds (5.0f);
			}
			Debug.Log ("coroutine");
		}
	}
	public void OnEnable() {
		StartCoroutine (DisplayTips ());
		buttonUpgrade.transform.FindChild ("Text").GetComponent<Text> ().text = 
			"요새 업그레이드" + "\r\n" +
			"<size=14>(" + (GameManager.Instance.citadel.upgradeCost * GameManager.Instance.citadel.level).ToString() + " G)</size>";
	}
}