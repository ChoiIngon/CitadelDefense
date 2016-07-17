using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelPlay : MonoBehaviour {
	public Button buttonSpeedup;
	// Use this for initialization
	void Start () {
		buttonSpeedup.onClick.AddListener (() => {
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
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
