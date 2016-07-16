using UnityEngine;
using UnityEngine.UI;
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

	public enum GameState
    {
        Ready,
        Play
    }
    
	public enum WaveResult
	{
		Win,
		Lose
	}

    public GameState state;
    
	public EnemyManager enemyManager;
	public Transform creatures;

    public PanelLobby 		lobbyPanel;
	public PanelUnitShop 	panelUnitShop;
	public PanelUnitInfo	unitInfoPanel;
	public ProgressBar 		hp;
	public ProgressBar 		mp;
	public ProgressBar 		waveProgress;

	public int waveLevel;
	public decimal gold {
		get { return _gold; }
		set { 
			_gold = value;
			textGold.text = _gold.ToString ("N0");
		}
	}
	private decimal _gold;
	public Text textGold;
	public const int WAVE_TIME = 40;
	public CitadelUnit citadel;
    public BuildingUnit[] buildings;
	public TowerUnit[] towers;
	public HeroUnit[] heros;
    public UnitSlot[] slots;
	[HideInInspector]
	public UnitSlot selectedSlot;
	[HideInInspector]
	public HeroUnit selectedUnit;

	public UIMessageBox messageBox;
	void Start () {
		gold = 100000;
		state = GameState.Ready;
		selectedSlot = null;
		selectedUnit = null;
        
        Transform transHeros = transform.FindChild("Unit/Heros");
        heros = new HeroUnit[transHeros.childCount];
        for (int i = 0; i < transHeros.childCount; i++)
        {
            heros[i] = transHeros.GetChild(i).GetComponent<HeroUnit>();
            heros[i].Init();
        }

		waveProgress.transform.FindChild ("Text").GetComponent<Text> ().text = "WAVE " + waveLevel;
	}

	private Wave wave = null;
	private IEnumerator waveCoroutine = null;
    public void WaveStart()
    {
        Debug.Log("Wave Started");
        lobbyPanel.gameObject.SetActive(false);
		state = GameState.Play;

		citadel.Init ();
        foreach(HeroUnit hero in heros)
        {
            Transform touchEvent = hero.transform.FindChild("TouchEvent");
            if(null != touchEvent)
            {
                touchEvent.gameObject.SetActive(true);
            }
        }

        foreach(UnitSlot slot in slots)
        {
            Transform touchEvent = slot.transform.FindChild("TouchEvent");
            if (null != touchEvent)
            {
                touchEvent.gameObject.SetActive(false);
            }
        }

		wave = new Wave ();
		waveCoroutine = wave.WaveStart ();
		StartCoroutine (waveCoroutine);
    }

	public void WaveEnd(WaveResult result)
	{
		lobbyPanel.gameObject.SetActive (true);
		state = GameState.Ready;
		if (WaveResult.Win == result) {
			waveLevel += 1;
		} else {
			enemyManager.Clear ();
		}
		if (null != waveCoroutine) {
			StopCoroutine (waveCoroutine);
		}
		wave = null;

        foreach (HeroUnit hero in heros)
        {
            Transform touchEvent = hero.transform.FindChild("TouchEvent");
            if (null != touchEvent)
            {
                touchEvent.gameObject.SetActive(false);
            }
        }

        foreach (UnitSlot slot in slots)
        {
            Transform touchEvent = slot.transform.FindChild("TouchEvent");
            if (null != touchEvent)
            {
               touchEvent.gameObject.SetActive(true);
            }
        }

		while (0 < creatures.childCount) {
			Transform child = creatures.GetChild (0);
			child.SetParent (null);
			Destroy (child.gameObject);
		}
		waveProgress.transform.FindChild ("Text").GetComponent<Text> ().text = "WAVE " + waveLevel;
		waveProgress.progress = 1.0f;

		citadel.hp.value = citadel.hp.max;
		citadel.mp.value = citadel.mp.max;
    }

	void Update()
	{
		hp.progress = (float)citadel.hp / (float)citadel.hp.max;
		mp.progress = (float)citadel.mp / (float)citadel.mp.max;
		hp.transform.FindChild("Text").GetComponent<Text>().text = citadel.hp.value + "/" + citadel.hp.max;
		mp.transform.FindChild("Text").GetComponent<Text>().text = citadel.mp.value + "/" + citadel.mp.max;
		if (null != wave) {
			wave.Update ();
			waveProgress.progress = wave.remainTime / GameManager.WAVE_TIME;
		}
	}
}
