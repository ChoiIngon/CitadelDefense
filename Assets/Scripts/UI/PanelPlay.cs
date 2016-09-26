using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelPlay : MonoBehaviour {
	public Button buttonBack;
	public ToggleButton buttonSpeed;

	// Use this for initialization
	void Start () {
		buttonSpeed.onValueChanged.AddListener (value => {
			if(GameManager.Instance.timeScale > 1.0f)
			{
				GameManager.Instance.timeScale = 1.0f;
			}
			else
			{
				GameManager.Instance.timeScale = 2.0f;
			}
			Time.timeScale = GameManager.Instance.timeScale;
		});
		buttonBack.onClick.AddListener (() => {
			GameManager.Instance.WaveEnd(GameManager.WaveResult.Lose);
		});
	}
}
