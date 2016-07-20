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

	public const int WAVE_TIME = 90;
	public int waveLevel;
	public float timeScale;
	public GameState gameState;
    
    public EnemyManager enemyManager;
	public Transform creatures;

	public PanelLobby 		uiLobbyPanel;
	public PanelPlay 		uiPlayPanel;
	public PanelHeroShop 	uiHeroShopPanel;
	public PanelHeroInfo	uiHeroInfoPanel;
	public ProgressBar 		uiCitadelHealth;
	public ProgressBar 		uiCitadelMana;
	public ProgressBar 		uiWaveProgress;
	public Text 			uiGold;
	public MessageBox 		uiMessageBox;

	public decimal gold {
		get { return _gold; }
		set { 
			_gold = value;
			uiGold.text = _gold.ToString ("N0");
		}
	}
	private decimal _gold;

	public CitadelUnit citadel;
	public HeroUnit[] heros;
    
	[HideInInspector]
	public UnitSlot selectedSlot;
	[HideInInspector]
	public HeroUnit selectedUnit;
	private Wave wave = null;
	private IEnumerator waveCoroutine = null;

	void Start () {
		gold = 100000;
		gameState = GameState.Ready;
		selectedSlot = null;
		selectedUnit = null;
		timeScale = 1.0f;
		Time.timeScale = timeScale;

        Transform transHeros = transform.FindChild("Unit/Heros");
        heros = new HeroUnit[transHeros.childCount];
        for (int i = 0; i < transHeros.childCount; i++)
        {
            heros[i] = transHeros.GetChild(i).GetComponent<HeroUnit>();
        }

		uiWaveProgress.transform.FindChild ("Text").GetComponent<Text> ().text = "WAVE " + waveLevel;

		// saved game data load
	}

	public void WaveStart()
    {
		uiLobbyPanel.gameObject.SetActive(false);
		uiPlayPanel.gameObject.SetActive (true);
		gameState = GameState.Play;

		citadel.Init ();
        foreach(HeroUnit hero in heros)
        {
            Transform touchEvent = hero.transform.FindChild("TouchEvent");
            if(null != touchEvent)
            {
                touchEvent.gameObject.SetActive(true);
            }
        }

        foreach(UnitSlot slot in citadel.slots)
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
		uiLobbyPanel.gameObject.SetActive (true);
		uiPlayPanel.gameObject.SetActive (false);
		gameState = GameState.Ready;
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

			hero.Init ();
        }

        foreach (UnitSlot slot in citadel.slots)
        {
            Transform touchEvent = slot.transform.FindChild("TouchEvent");
            if (null != touchEvent)
            {
               touchEvent.gameObject.SetActive(true);
            }
        }

		uiWaveProgress.transform.FindChild ("Text").GetComponent<Text> ().text = "WAVE " + waveLevel;
		uiWaveProgress.progress = 1.0f;

		citadel.hp.value = citadel.hp.max;
		citadel.mp.value = citadel.mp.max;
    }

	void Update()
	{
		uiCitadelHealth.progress = (float)citadel.hp / (float)citadel.hp.max;
		uiCitadelHealth.transform.FindChild("Text").GetComponent<Text>().text = citadel.hp.value + "/" + citadel.hp.max;

		uiCitadelMana.progress = (float)citadel.mp / (float)citadel.mp.max;
		uiCitadelMana.transform.FindChild("Text").GetComponent<Text>().text = citadel.mp.value + "/" + citadel.mp.max;

		if (null != wave) {
			wave.Update ();
			uiWaveProgress.progress = wave.remainTime / GameManager.WAVE_TIME;
		}
	}
}
