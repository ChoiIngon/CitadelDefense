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
    public LobbyPanel lobbyPanel;

    public UnitCitadel citadel;

	public BuildingUnit[] buildings;
	public TowerUnit[] towers;
	public HeroUnit[] heros;
    
	public PanelUnitShop 	unitShopPanel;
	public PanelUnitInfo	unitInfoPanel;

	[HideInInspector]
	public UnitSlot selectedSlot;
	[HideInInspector]
	public BasePlayerUnit selectedUnit;
	// Use this for initialization
	void Start () {
        state = GameState.Lobby;
		selectedUnit = null;
	}

    public void WaveStart()
    {
        Debug.Log("Wave Started");
        lobbyPanel.gameObject.SetActive(false);
        state = GameState.Play;
    }

	public void WaveEnd(WaveResult result)
	{
		lobbyPanel.gameObject.SetActive (true);
		state = GameState.Lobby;
	}
}
