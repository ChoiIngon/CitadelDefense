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
        Lobby,
        Play
    }
    
	public enum WaveResult
	{
		Win,
		Lose
	}
    public GameState state;
    
	public EnemyManager enemyManager;

    public PanelLobby 		lobbyPanel;
	public PanelUnitShop 	unitShopPanel;
	public PanelUnitInfo	unitInfoPanel;

	public int waveLevel;
	public int gold {
		get { return _gold; }
		set { 
			_gold = value;
			textGold.text = string.Format ("{0:N}", _gold);
		}
	}
	private int _gold;
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
	public BasePlayerUnit selectedUnit;

	void Start () {
		gold = 9999999;
        state = GameState.Lobby;
		selectedSlot = null;
		selectedUnit = null;
        
        Transform transHeros = transform.FindChild("Unit/Heros");
        heros = new HeroUnit[transHeros.childCount];
        for (int i = 0; i < transHeros.childCount; i++)
        {
            heros[i] = transHeros.GetChild(i).GetComponent<HeroUnit>();
            heros[i].Init();
        }
	}

    public void WaveStart()
    {
        Debug.Log("Wave Started");
        lobbyPanel.gameObject.SetActive(false);
		wave = new Wave ();
        state = GameState.Play;

		citadel.hp.max = 100 + citadel.level * 100;
		citadel.hp.value = citadel.hp.max;
		citadel.mp.max = 500 + citadel.level * 10;
		citadel.mp.value = citadel.mp.max;

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
    }

	public void WaveEnd(WaveResult result)
	{
		lobbyPanel.gameObject.SetActive (true);
		state = GameState.Lobby;
		if (WaveResult.Win == result) {
			waveLevel += 1;
		} else {
			enemyManager.Clear ();
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
    }

	public class Wave
	{
		public int remainTime = GameManager.WAVE_TIME;
		private float deltaTime = 0.0f;

		public void Update()
		{
			if (1.0f <= deltaTime) {
				remainTime -= 1;
				remainTime = Mathf.Max (0, remainTime);
				if (0 == remainTime) {
					if (0 == GameManager.Instance.enemyManager.transform.childCount) {
						GameManager.Instance.WaveEnd (GameManager.WaveResult.Win);
					}
					return;
				}
				deltaTime = 0.0f;
				for(int i=0; i<GameManager.Instance.enemyManager.formations.Length; i++)
				{
					int index = Random.Range (0, GameManager.Instance.enemyManager.formations.Length);
					EnemyManager.EnemyFormation formation = GameManager.Instance.enemyManager.formations [index];
					if (formation.firstWave > GameManager.Instance.waveLevel) {
						return;
					}
					foreach (Vector3 position in formation.positions) {
						EnemyUnit unitEnemy = (EnemyUnit)GameObject.Instantiate<EnemyUnit> (formation.enemy);
						unitEnemy.Init();
						unitEnemy.transform.position = GameManager.Instance.enemyManager.transform.position + position;
						unitEnemy.transform.SetParent (GameManager.Instance.enemyManager.transform);
					}
					break;
				}
			}
			deltaTime += Time.deltaTime;
		}
	}
	private Wave wave = null;
	void Update()
	{
		if (GameState.Lobby == state) {
		} else if (GameState.Play == state) {
			wave.Update ();
		}
	}
}
