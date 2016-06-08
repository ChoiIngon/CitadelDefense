using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LobbyPanel : MonoBehaviour {
    public Button advertisement;
    public Button waveStart;
	// Use this for initialization
	void Start () {
        waveStart.onClick.AddListener(() =>
        {
            GameManager.Instance.WaveStart();
        });
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
