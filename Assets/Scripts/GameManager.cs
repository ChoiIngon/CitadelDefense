using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
// for file save and load
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour {
	private const int SAVE_FORMAT_VERSION = 3;
	private const int DEFAULT_GOLD = 14000;
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
		public int version;
		public long gold;
		public int waveLevel;
		public int citadelLevel;
		public Dictionary<string, HeroUnit.SaveData> heros;
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

	public PanelCitadel 	uiCitadelPanel;
	public PanelCitadelBuff	uiCitadelBuffPanel;
	public ProgressBar 		uiCitadelHealth;
	public ProgressBar 		uiCitadelMana;

	public PanelPlay 		uiPlayPanel;
	public PanelHeroShop 	uiHeroShopPanel;
	public PanelHeroInfo	uiHeroInfoPanel;

	public GameObject 		uiItemPanel;

	public ProgressBar 		uiWaveProgress;
	public Text 			uiGold;
	public MessageBox 		uiMessageBox;
	public DialogBox 		uiDialogBox;
    public PanelResult      uiResultPanel;

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
		gold = DEFAULT_GOLD;
		gameState = GameState.Ready;
		selectedSlot = null;
		selectedUnit = null;
		timeScale = 1.0f;
		Time.timeScale = timeScale;

		citadel.Init ();
        
		Load();

        uiCitadelPanel.OnEnable();
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
		Time.timeScale = GameManager.Instance.timeScale;
		if (0 == citadel.GetActiveUnitCount ()) {
			uiMessageBox.message = "마법사를 먼저 성에 배치 해주세요.";
			return;
		}
		uiCitadelPanel.gameObject.SetActive(false);
		uiPlayPanel.gameObject.SetActive (true);
		gameState = GameState.Play;

		foreach(CitadelParts parts in citadel.citadelParts)
        {
			if (null != parts.slot.equippedUnit) 
			{
				HeroUnit unit = parts.slot.equippedUnit;
				unit.SetActive (true);
			}
			if (null != parts.slot.touch)
            {
				parts.slot.SetActive (false);
            }
        }

		wave = new Wave ();
		waveCoroutine = wave.WaveStart ();
		StartCoroutine (waveCoroutine);
    }

	public void WaveEnd(WaveResult result)
	{
		Time.timeScale = 1.0f;
		uiCitadelPanel.gameObject.SetActive (true);
		uiPlayPanel.gameObject.SetActive (false);
		gameState = GameState.Ready;
		if (WaveResult.Win == result) {
			waveLevel += 1;
		} else {
			enemyManager.Clear ();
		}

        uiResultPanel.Active(result);

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
				unit.SetActive (false);
			}

			if (null != parts.slot.touch)
            {
				parts.slot.SetActive (true);
            }
        }

		uiWaveProgress.transform.FindChild ("Text").GetComponent<Text> ().text = "WAVE " + waveLevel;
		uiWaveProgress.progress = 1.0f;

		while (0 < creatures.childCount) {
			Transform child = creatures.GetChild (0);
			child.SetParent (null);
			Object.Destroy (child.gameObject);
		}
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
		data.version = SAVE_FORMAT_VERSION;
		data.gold = gold;
		data.waveLevel = waveLevel;
		data.citadelLevel = citadel.level;

		data.heros = new Dictionary<string, HeroUnit.SaveData> ();
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
		Debug.Log (Application.persistentDataPath);
		if (File.Exists (Application.persistentDataPath + "/playerdata.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerdata.dat", FileMode.Open);
			SaveData data = (SaveData)bf.Deserialize (file);

			gold = data.gold;
			citadel.level = data.citadelLevel;

			if (data.version == SAVE_FORMAT_VERSION) {
				waveLevel = data.waveLevel;
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
			}
			file.Close ();
		} 
	}

	public void Quit()
	{
		if (GameManager.GameState.Ready == gameState) {
			uiDialogBox.Activate ("Quit?", () => {
				Application.Quit();
			});
		} else {
			uiDialogBox.Activate ("Exit?", () => {
				GameManager.Instance.WaveEnd (WaveResult.Lose);	
			});
		}
	}
}
