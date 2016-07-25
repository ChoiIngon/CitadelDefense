using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
		public HeroUnit.SaveData[] heros;
		public CitadelParts.SaveData[] citadelParts;
		public CitadelBuff.SaveData[] citadelBuffs;
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
	public GameObject 		uiCitadelBuffPanel;
	public GameObject 		uiItemPanel;
	public ProgressBar 		uiCitadelHealth;
	public ProgressBar 		uiCitadelMana;
	public ProgressBar 		uiWaveProgress;
	public Text 			uiGold;
	public MessageBox 		uiMessageBox;

	public long gold {
		get { return _gold; }
		set { 
			_gold = value;
			uiGold.text = _gold.ToString ("N0");
		}
	}
	[SerializeField]
	private long _gold;

	public CitadelUnit citadel;
	public HeroUnit[] heros;
    
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

        Transform transHeros = transform.FindChild("Unit/Heros");
        heros = new HeroUnit[transHeros.childCount];
        for (int i = 0; i < transHeros.childCount; i++)
        {
            heros[i] = transHeros.GetChild(i).GetComponent<HeroUnit>();
			heros[i].gameObject.SetActive (false);
        }

		Transform transCitadelParts = transform.FindChild ("Unit/Citadel/Animation/Parts");
		citadel.citadelParts = new CitadelParts[transCitadelParts.childCount];
		for (int i = 0; i < citadel.citadelParts.Length; i++) {
			CitadelParts parts = transCitadelParts.GetChild(i).GetComponent<CitadelParts>();
			parts.Init ();
			parts.gameObject.SetActive (false);
			citadel.citadelParts [parts.slotIndex] = parts;
		}

		Load();

        uiLobbyPanel.OnEnable();
		uiWaveProgress.transform.FindChild ("Text").GetComponent<Text> ().text = "WAVE " + waveLevel;
		foreach(HeroUnit hero in heros)
		{
			hero.Init ();
		}
		citadel.Init ();
	}

	public void WaveStart()
    {
		uiLobbyPanel.gameObject.SetActive(false);
		uiPlayPanel.gameObject.SetActive (true);
		gameState = GameState.Play;

		foreach(HeroUnit hero in heros)
        {
            if(null != hero.touch)
            {
				hero.touch.gameObject.SetActive(true);
            }
        }

		foreach(CitadelParts parts in citadel.citadelParts)
        {
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

        foreach (HeroUnit hero in heros)
        {
           	if (null != hero.touch)
            {
				hero.touch.gameObject.SetActive(false);
            }

			hero.Init ();
        }

		foreach (CitadelParts parts in citadel.citadelParts)
        {
			if (null != parts.slot.touch)
            {
				parts.slot.touch.gameObject.SetActive(true);
            }
        }

		uiWaveProgress.transform.FindChild ("Text").GetComponent<Text> ().text = "WAVE " + waveLevel;
		uiWaveProgress.progress = 1.0f;

		citadel.Init ();
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
		data.heros = new HeroUnit.SaveData[heros.Length];
		for (int i = 0; i < heros.Length; i++) {
			HeroUnit.SaveData hero = new HeroUnit.SaveData ();
			hero.level = heros [i].level;
			hero.purchased = heros [i].purchased;
			hero.slotIndex = heros [i].slotIndex;
			hero.equiped = heros [i].equiped;
			data.heros [i] = hero;
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

			for (int i = 0; i < heros.Length; i++) {
				HeroUnit hero = heros [i];
				hero.level = data.heros [i].level;
				hero.purchased = data.heros [i].purchased;
				hero.slotIndex = data.heros [i].slotIndex;
				hero.equiped = data.heros [i].equiped;
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
			for (int i = 0; i < citadel.citadelBuffs.Length; i++) {
				CitadelBuff buff = citadel.citadelBuffs [i];
				if (null == data.citadelBuffs [i]) {
					continue;
				}
				buff.level = data.citadelBuffs [i].level;
			}
			file.Close ();
		}
	}
}
