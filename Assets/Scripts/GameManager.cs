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

	public int wave;
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

	[HideInInspector]
	public UnitSlot selectedSlot;
	[HideInInspector]
	public BasePlayerUnit selectedUnit;

	void Start () {
		gold = 9999999;
        state = GameState.Lobby;
		selectedSlot = null;
		selectedUnit = null;
        for(int i=0; i<heros.Length; i++)
        {
            heros[i].Init();
        }
	}

    public void WaveStart()
    {
        Debug.Log("Wave Started");
        lobbyPanel.gameObject.SetActive(false);
        state = GameState.Play;

		citadel.hp.max = 100 + citadel.level * 100;
		citadel.hp.value = citadel.hp.max;
		citadel.mp.max = 500 + citadel.level * 10;
		citadel.mp.value = citadel.mp.max;

		enemyManager.gameObject.SetActive (true);
    }

	public void WaveEnd(WaveResult result)
	{
		lobbyPanel.gameObject.SetActive (true);
		state = GameState.Lobby;
        if(WaveResult.Win == result)
        {
            wave += 1;
        }
	}
}
