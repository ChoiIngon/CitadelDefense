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
	public CitadelUnit citadel;
    public BuildingUnit[] buildings;
	public TowerUnit[] towers;
	public HeroUnit[] heros;
	public EnemyUnit[] enemies;
    
	[HideInInspector]
	public UnitSlot selectedSlot;
	[HideInInspector]
	public BasePlayerUnit selectedUnit;

	void Start () {
        state = GameState.Lobby;
		selectedSlot = null;
		selectedUnit = null;
	}

    public void WaveStart()
    {
        Debug.Log("Wave Started");
        lobbyPanel.gameObject.SetActive(false);
        state = GameState.Play;

		citadel.hp.max = 1000 + citadel.level * 100;
		citadel.hp.value = citadel.hp.max;
		citadel.mp.max = 500 + citadel.level * 10;
		citadel.mp.value = citadel.mp.max;
    }

	public void WaveEnd(WaveResult result)
	{
		lobbyPanel.gameObject.SetActive (true);
		state = GameState.Lobby;
	}
}
