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

    public GameObject failPopup;

    public GameState state;
    
    public EnemyManager enemyManager;
    public LobbyPanel lobbyPanel;
	public HeroInfoPanel heroInfoPanel;
    public UnitCitadel citadel;
	public UnitHero[] heros;
    public UnitBuilding[] buildings;
    public UnitTurret[] turrets;


	// Use this for initialization
	void Start () {
        state = GameState.Lobby;
        failPopup.SetActive(false);
	}

    public void WaveStart()
    {
        Debug.Log("Wave Started");
        lobbyPanel.gameObject.SetActive(false);
        state = GameState.Play;
    }

    public void PurchaseHero(int index)
    {
        UnitHero hero = heros[index];
        if(hero.price > citadel.gold)
        {
            // not enough gold message
            return;
        }

        hero.purchased = true;
    }
}
