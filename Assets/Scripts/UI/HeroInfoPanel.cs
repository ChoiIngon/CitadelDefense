using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroInfoPanel : MonoBehaviour {
    public UnitHero unit;
	// Use this for initialization
    private Button buyButton { get { return transform.FindChild("BuyButton").GetComponent<Button>(); }}
    private Button equipButton { get { return transform.FindChild("EquipButton").GetComponent<Button>(); } }
    private Button levelUpButton { get { return transform.FindChild("LevelUpButton").GetComponent<Button>(); } }
    private Button closeButton { get { return transform.FindChild("CloseButton").GetComponent<Button>(); } }
    private Text heroName { get { return transform.FindChild("HeroName").GetComponent<Text>(); } }
    private Text heroLevel { get { return transform.FindChild("HeroLevel").GetComponent<Text>(); } }

    private Image attackPowerImage { get { return transform.FindChild("AttackPowerImage").GetComponent<Image>(); } }
    private Text attackPowerText { get { return transform.FindChild("AttackPowerText").GetComponent<Text>(); } }

    private Image specialAttackImage { get { return transform.FindChild("SpecialAttackImage").GetComponent<Image>(); } }
    private Text specialAttackText { get { return transform.FindChild("SpecialAttackText").GetComponent<Text>(); } }

    private Image skillImage { get { return transform.FindChild("SkillPanel/SkillImage").GetComponent<Image>(); } }
    private Text skillName { get { return transform.FindChild("SkillPanel/SkillName").GetComponent<Text>(); } }
    private Text skillDescription { get { return transform.FindChild("SkillPanel/SkillDescription").GetComponent<Text>(); } }

	public void SetHeroUnit(UnitHero unit)
    {

    }
}
