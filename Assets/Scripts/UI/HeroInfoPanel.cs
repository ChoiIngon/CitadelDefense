using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroInfoPanel : MonoBehaviour {
	[HideInInspector]
    public UnitHero unit;
	const string defaultChildPath = "Background/";
	// Use this for initialization
    private Button buyButton { get { return transform.FindChild(defaultChildPath + "BuyButton").GetComponent<Button>(); }}
	private Button equipButton { get { return transform.FindChild(defaultChildPath + "EquipButton").GetComponent<Button>(); } }
	private Button levelUpButton { get { return transform.FindChild(defaultChildPath + "LevelUpButton").GetComponent<Button>(); } }
	private Button closeButton { get { return transform.FindChild(defaultChildPath + "CloseButton").GetComponent<Button>(); } }
	private Text heroName { get { return transform.FindChild(defaultChildPath + "HeroName").GetComponent<Text>(); } }
	private Text heroLevel { get { return transform.FindChild(defaultChildPath + "HeroLevel").GetComponent<Text>(); } }

	private Image attackPowerImage { get { return transform.FindChild(defaultChildPath + "AttackPowerImage").GetComponent<Image>(); } }
	private Text attackPowerText { get { return transform.FindChild(defaultChildPath + "AttackPowerText").GetComponent<Text>(); } }

	private Image specialAttackImage { get { return transform.FindChild(defaultChildPath + "SpecialAttackImage").GetComponent<Image>(); } }
	private Text specialAttackText { get { return transform.FindChild(defaultChildPath + "SpecialAttackText").GetComponent<Text>(); } }

	private Image skillImage { get { return transform.FindChild(defaultChildPath + "SkillPanel/SkillImage").GetComponent<Image>(); } }
	private Text skillName { get { return transform.FindChild(defaultChildPath + "SkillPanel/SkillName").GetComponent<Text>(); } }
	private Text skillDescription { get { return transform.FindChild(defaultChildPath + "SkillPanel/SkillDescription").GetComponent<Text>(); } }

	public void SetHeroUnit(UnitHero unit)
    {
		closeButton.onClick.AddListener (() => {
			gameObject.SetActive(false);
		});
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
			buyButton.onClick.AddListener (() => {
			});
		} else {
		}
    }
}
