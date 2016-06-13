using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroInfoPanel : MonoBehaviour {
	[HideInInspector]
    public UnitHero unit;
	const string defaultChildPath = "Background/";
	// Use this for initialization
	public Button buyButton { get { return transform.FindChild(defaultChildPath + "BuyButton").GetComponent<Button>(); }}
	public Button equipButton { get { return transform.FindChild(defaultChildPath + "EquipButton").GetComponent<Button>(); } }
	public Button levelUpButton { get { return transform.FindChild(defaultChildPath + "LevelUpButton").GetComponent<Button>(); } }
	public Text heroName { get { return transform.FindChild(defaultChildPath + "HeroName").GetComponent<Text>(); } }
	public Text heroLevel { get { return transform.FindChild(defaultChildPath + "HeroLevel").GetComponent<Text>(); } }

	public Image attackPowerImage { get { return transform.FindChild(defaultChildPath + "AttackPowerImage").GetComponent<Image>(); } }
	public Text attackPowerText { get { return transform.FindChild(defaultChildPath + "AttackPowerText").GetComponent<Text>(); } }

	public Image specialAttackImage { get { return transform.FindChild(defaultChildPath + "SpecialAttackImage").GetComponent<Image>(); } }
	public Text specialAttackText { get { return transform.FindChild(defaultChildPath + "SpecialAttackText").GetComponent<Text>(); } }

	public Image skillImage { get { return transform.FindChild(defaultChildPath + "SkillPanel/SkillImage").GetComponent<Image>(); } }
	public Text skillName { get { return transform.FindChild(defaultChildPath + "SkillPanel/SkillName").GetComponent<Text>(); } }
	public Text skillDescription { get { return transform.FindChild(defaultChildPath + "SkillPanel/SkillDescription").GetComponent<Text>(); } }

	public void Start()
	{
		buyButton.onClick.AddListener (() => {
			if(null != GameManager.Instance.selectedSlot.unit)
			{
				GameManager.Instance.selectedSlot.unit.slotIndex = -1;
				GameManager.Instance.selectedSlot.unit.gameObject.SetActive(false);
				GameManager.Instance.selectedSlot.unit = null;
			}

			unit.slotIndex = GameManager.Instance.selectedSlot.slotIndex;
			unit.transform.position = GameManager.Instance.selectedSlot.transform.position;
			unit.gameObject.SetActive(true);
			GameManager.Instance.selectedSlot.unit = unit;
			GameManager.Instance.heroInfoPanel.gameObject.SetActive(false);
			GameManager.Instance.heroPanel.gameObject.SetActive(false);
		});

		equipButton.onClick.AddListener (() => {
			if(null != GameManager.Instance.selectedSlot.unit)
			{
				GameManager.Instance.selectedSlot.unit.slotIndex = -1;
				GameManager.Instance.selectedSlot.unit.gameObject.SetActive(false);
				GameManager.Instance.selectedSlot.unit = null;
			}

			if(-1 != GameManager.Instance.selectedHero.slotIndex)
			{
				GameManager.Instance.heroSlots[GameManager.Instance.selectedHero.slotIndex].unit = null;
			}

			unit.slotIndex = GameManager.Instance.selectedSlot.slotIndex;
			unit.transform.position = GameManager.Instance.selectedSlot.transform.position;
			unit.gameObject.SetActive(true);
			GameManager.Instance.selectedSlot.unit = unit;
			GameManager.Instance.heroInfoPanel.gameObject.SetActive(false);
			GameManager.Instance.heroPanel.gameObject.SetActive(false);
		});

		levelUpButton.onClick.AddListener (() => {
			unit.level += 1;
			Debug.Log("hero unit level:" + unit.level);
		});
	}
	public void SetHeroUnit(UnitHero unit)
    {
		GameManager.Instance.selectedHero = unit;
		this.unit = unit;
		heroName.text = unit.name;
		heroLevel.text = unit.level.ToString ();

		attackPowerImage.sprite = unit.normalAttackInfo.sprite;
		specialAttackImage.sprite = unit.specialAttackInfo.sprite;
		skillName.text = unit.specialAttackInfo.name;
		buyButton.gameObject.SetActive (false);
		equipButton.gameObject.SetActive (false);
		levelUpButton.gameObject.SetActive (false);
		if (false == unit.purchased) {
			buyButton.gameObject.SetActive (true);
		} else {
			levelUpButton.gameObject.SetActive (true);
			if (unit.slotIndex != GameManager.Instance.selectedSlot.slotIndex) {
				equipButton.gameObject.SetActive (true);
			}
		}
    }
}
