using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelCitadelBuff : MonoBehaviour {
	public ContentCitadelBuff contentPrefab;
	public ContentCitadelBuff selectedBuff;
	public Transform content;
	public Text description;
	public Button upgrade;

	void Start()
	{
		upgrade.onClick.AddListener (() => {
			if(null != selectedBuff)
			{
				selectedBuff.Upgrade();
				GameManager.Instance.Save();
			}
		});

		foreach(CitadelBuff buff in GameManager.Instance.citadel.citadelBuffs)
		{
			ContentCitadelBuff contentCitadelBuff = GameObject.Instantiate<ContentCitadelBuff>(contentPrefab);
			contentCitadelBuff.panel = this;
			contentCitadelBuff.buff = buff;
			contentCitadelBuff.transform.SetParent(content);
			contentCitadelBuff.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		}
	}
}
