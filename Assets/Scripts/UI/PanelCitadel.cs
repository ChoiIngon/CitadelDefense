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

    private void Awake()
    {
        Util.EventSystem.Subscribe<GameManager>(EventID.GameStart, OnGameStart);
        Util.EventSystem.Subscribe(EventID.WaveStart, OnWaveStart);
        Util.EventSystem.Subscribe(EventID.WaveEnd, OnWaveEnd);
    }
    private void OnGameStart (GameManager gameManager) {
        if(null == waveStart)
        {
            throw new System.Exception("can not find 'WaveStart' button");
        }
        if (null == buttonUpgrade)
        {
            throw new System.Exception("can not find 'Upgrade' button");
        }
        if (null == buttonBuff)
        {
            throw new System.Exception("can not find 'Buff' button");
        }
        if (null == buttonItem)
        {
            throw new System.Exception("can not find 'Item' button");
        }
        if (null == buttonExit)
        {
            throw new System.Exception("can not find 'Exit' button");
        }
        if (null == textTip)
        {
            throw new System.Exception("can not find 'Tip' Text");
        }
            
        waveStart.onClick.AddListener(GameManager.Instance.WaveStart);
		OnWaveEnd();

		buttonUpgrade.onClick.AddListener (() => {
			GameManager.Instance.citadel.Upgrade();
			buttonUpgrade.transform.Find ("Text").GetComponent<Text> ().text = 
				GameText.Instance.GetText("UPGRADE_CITADEL") + "\r\n" +
				"<size=14>(" + (GameManager.Instance.citadel.upgradeCost * GameManager.Instance.citadel.level).ToString() + " G)</size>";
		});
		buttonBuff.onClick.AddListener (() => {
			if(10 > GameManager.Instance.waveLevel)
			{
				GameManager.Instance.uiMessageBox.message = "10 웨이브 이후 부터 사용 가능 합니다.";
				return;
			}
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
		}
	}
	private void OnWaveStart()
    {
        gameObject.SetActive(false);
    }
    private void OnWaveEnd()
    {
        gameObject.SetActive(true);
        StartCoroutine(DisplayTips());
        buttonUpgrade.transform.Find("Text").GetComponent<Text>().text =
			GameText.Instance.GetText("UPGRADE_CITADEL") + "\r\n" +
            "<size=14>(" + (GameManager.Instance.citadel.upgradeCost * GameManager.Instance.citadel.level).ToString() + " G)</size>";
    }
}