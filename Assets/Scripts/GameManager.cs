using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    private static GameManager self;

    public static GameManager Instance
    {
        get
        {
            if(null == self)
            {
                self = FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return self;
        }
    }
    public GameObject failPopup;
	public enum State {
		Setting,
		Play
	}
	public State state;
	// Use this for initialization
	void Start () {
		state = State.Setting;
        failPopup.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
