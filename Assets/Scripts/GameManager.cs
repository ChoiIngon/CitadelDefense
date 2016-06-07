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

    public enum State
    {
        Setting,
        Play
    }

    public GameObject failPopup;

    public State state;
    public UnitCitadel citadel;
    public GameObject enemyManager;
    public GameObject ui;	
    
	// Use this for initialization
	void Start () {
		state = State.Setting;
        failPopup.SetActive(false);
	}
}
