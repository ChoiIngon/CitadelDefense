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

    public UnitBuilding[] buildings;
    public UnitTurret[] turrets;


	public UnitHero[] 	 heros;
	public HeroSlot[] 	 heroSlots { get { return citadel.transform.GetComponentsInChildren<HeroSlot> (); } }
	public HeroPanel 	 heroPanel;
	public HeroInfoPanel heroInfoPanel;

	public HeroSlot selectedSlot;
	public UnitHero selectedHero;
	// Use this for initialization
	void Start () {
        state = GameState.Lobby;
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
