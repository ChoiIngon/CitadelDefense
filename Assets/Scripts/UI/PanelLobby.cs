using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelLobby : MonoBehaviour {
    public Button advertisement;
    public Button waveStart;
	public Button buttonUpgradeCitadel;
	// Use this for initialization
	void Start () {
        waveStart.onClick.AddListener(GameManager.Instance.WaveStart);
		buttonUpgradeCitadel.onClick.AddListener (GameManager.Instance.citadel.Upgrade);
	}
}
