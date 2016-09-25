using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelPlay : MonoBehaviour {
	public Button buttonBack;
	public Button buttonSpeed;

	// Use this for initialization
	void Start () {
		buttonSpeed.onClick.AddListener (() => {
			if(GameManager.Instance.timeScale > 1.0f)
			{
				GameManager.Instance.timeScale = 1.0f;
			}
			else
			{
				GameManager.Instance.timeScale = 2.0f;
				buttonSpeed.Select();
			}
			Time.timeScale = GameManager.Instance.timeScale;
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
