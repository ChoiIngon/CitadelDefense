using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
// for file save and load
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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

	[System.Serializable]
	public class SaveData {
		public int waveLevel;
		public long gold;
		public int citadelLevel;
		public Dictionary<int, HeroUnit.SaveData> heros;
		public CitadelParts.SaveData[] citadelParts;
		public CitadelBuff.SaveData[] citadelBuffs;
	}
	public const int WAVE_TIME = 90;
	public int waveLevel;
	public float timeScale;
	public GameState gameState;
    
    public EnemyManager enemyManager;
	public Transform creatures;

	public Dictionary<Object, Object> enemies = new Dictionary<Object, Object>();
	public Dictionary<Object, Object> player = new Dictionary<Object, Object>();

	public PanelLobby 		uiLobbyPanel;
	public PanelPlay 		uiPlayPanel;
	public PanelHeroShop 	uiHeroShopPanel;
	public PanelHeroInfo	uiHeroInfoPanel;
	public GameObject 		uiCitadelBuffPanel;
	public GameObject 		uiItemPanel;
	public ProgressBar 		uiCitadelHealth;
	public ProgressBar 		uiCitadelMana;
	public ProgressBar 		uiWaveProgress;
	public Text 			uiGold;
	public MessageBox 		uiMessageBox;

	public GameObject 		effectGoldReward;
	public long gold {
		get { return _gold; }
		set { 
			_gold = value;
			uiGold.text = _gold.ToString ("N0");
		}
	}
	[SerializeField]
	private long _gold;
	public float goldBonus;
	public CitadelUnit citadel;

	[HideInInspector]
	public UnitSlot selectedSlot;
	[HideInInspector]
	public HeroUnit selectedUnit;
	private Wave wave = null;
	private IEnumerator waveCoroutine = null;

	void Start () {
		gold = 10000;
		gameState = GameState.Ready;
		selectedSlot = null;
		selectedUnit = null;
		timeScale = 1.0f;
		Time.timeScale = timeScale;

		citadel.Init ();
        
		Load();

        uiLobbyPanel.OnEnable();
		uiWaveProgress.transform.FindChild ("Text").GetComponent<Text> ().text = "WAVE " + waveLevel;
		foreach(var itr in citadel.heros)
		{
			HeroUnit hero = itr.Value;
			hero.Init ();
		}
		citadel.Reset ();
	}

	public void WaveStart()
    {
		uiLobbyPanel.gameObject.SetActive(false);
		uiPlayPanel.gameObject.SetActive (true);
		gameState = GameState.Play;

		foreach(CitadelParts parts in citadel.citadelParts)
        {
			if (null != parts.slot.equippedUnit) 
			{
				HeroUnit unit = parts.slot.equippedUnit;
				if (null != unit.touch) 
				{
					unit.touch.gameObject.SetActive (true);
				}
			}
			if (null != parts.slot.touch)
            {
				parts.slot.touch.gameObject.SetActive(false);
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

		foreach (CitadelParts parts in citadel.citadelParts)
        {
			if (null != parts.slot.equippedUnit) 
			{
				HeroUnit unit = parts.slot.equippedUnit;
				unit.Init ();
				if (null != unit.touch) 
				{
					unit.touch.gameObject.SetActive (false);
				}
			}

			if (null != parts.slot.touch)
            {
				parts.slot.touch.gameObject.SetActive(true);
            }
        }

		uiWaveProgress.transform.FindChild ("Text").GetComponent<Text> ().text = "WAVE " + waveLevel;
		uiWaveProgress.progress = 1.0f;

		citadel.Reset ();
		Save ();
    }

	void Update()
	{
		uiCitadelHealth.progress = (float)citadel.health / (float)citadel.health.max;
		uiCitadelHealth.transform.FindChild("Text").GetComponent<Text>().text = citadel.health.value + "/" + citadel.health.max;

		uiCitadelMana.progress = (float)citadel.mana / (float)citadel.mana.max;
		uiCitadelMana.transform.FindChild("Text").GetComponent<Text>().text = citadel.mana.value + "/" + citadel.mana.max;

		if (null != wave) {
			wave.Update ();
			uiWaveProgress.progress = wave.remainTime / GameManager.WAVE_TIME;
		}
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerdata.dat");

		SaveData data = new SaveData ();
		data.waveLevel = waveLevel;
		data.citadelLevel = citadel.level;
		data.gold = gold;
		data.heros = new Dictionary<int, HeroUnit.SaveData> ();
		foreach (var itr in citadel.heros) {
			HeroUnit hero = itr.Value;
			if (false == hero.purchased) {
				continue;
			}
			HeroUnit.SaveData saveData = new HeroUnit.SaveData ();
			saveData.id = hero.info.id;
			saveData.level = hero.level;
			saveData.purchased = hero.purchased;
			saveData.slotIndex = hero.slotIndex;
			saveData.equiped = hero.equiped;
			data.heros [hero.info.id] = saveData;
		}

		data.citadelParts = new CitadelParts.SaveData[citadel.citadelParts.Length];
		for (int i = 0; i < citadel.citadelParts.Length; i++) {
			CitadelParts.SaveData saveData = new CitadelParts.SaveData ();
			saveData.active = citadel.citadelParts [i].gameObject.activeSelf;
			data.citadelParts [i] = saveData;
		}

		data.citadelBuffs = new CitadelBuff.SaveData[citadel.citadelBuffs.Length];
		for (int i = 0; i < citadel.citadelBuffs.Length; i++) {
			CitadelBuff.SaveData saveData = new CitadelBuff.SaveData ();
			saveData.level = citadel.citadelBuffs [i].level;
			data.citadelBuffs [i] = saveData;
		}
		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/playerdata.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerdata.dat", FileMode.Open);
			SaveData data = (SaveData)bf.Deserialize (file);

			waveLevel = data.waveLevel;
			gold = data.gold;
			citadel.level = data.citadelLevel;

			foreach (var itr in data.heros) {
				HeroUnit.SaveData saveData = itr.Value;
				HeroUnit hero = citadel.heros [saveData.id];
				hero.level = saveData.level;
				hero.purchased = saveData.purchased;
				hero.slotIndex = saveData.slotIndex;
				hero.equiped = saveData.equiped;
				if (true == hero.equiped) {
					citadel.citadelParts [hero.slotIndex].slot.EquipUnit (hero);
				}
			}

			if (null != data.citadelParts) {
				for (int i = 0; i < citadel.citadelParts.Length; i++) {
					if (null == data.citadelParts [i]) {
						continue;
					}
					if (true == data.citadelParts [i].active) {
						citadel.citadelParts [i].gameObject.SetActive (true);
					}
				}
			}
			for (int i = 0; i < data.citadelBuffs.Length; i++) {
				CitadelBuff buff = citadel.citadelBuffs [i];
				buff.level = data.citadelBuffs [i].level;
			}
			file.Close ();
		}
	}
}
