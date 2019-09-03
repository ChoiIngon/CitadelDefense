using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelHeroInfo : MonoBehaviour {
	// Use this for initialization
	public Button buttonBuy;
	public Button buttonEquip;
	public Button buttonLevelup;
	public Image imageUnit;
	public Text textName;
	public Text textLevel;
	public Text textAttackPower;
	public Text textAttackSpeed;
	public Text textCritical;

	public ContentHeroShop contentHeroShop;

	public ContentSkillInfo passiveSkill;
	public ContentSkillInfo activeSkill;

	public void OnEnable()
	{
		Init ();
	}

	public void Start()
	{
		buttonBuy.onClick.AddListener (() => {
			if(null == GameManager.Instance.selectedUnit)
			{
				return;
			}

			HeroUnit unit = GameManager.Instance.selectedUnit;
			if(GameManager.Instance.gold < unit.info.purchasePrice)
			{
				GameManager.Instance.uiMessageBox.message = "골드가 부족 합니다";
				return;
			}
			GameManager.Instance.gold -= unit.info.purchasePrice;
			unit.purchased = true;
			GameManager.Instance.selectedSlot.EquipUnit(unit);

			GameManager.Instance.selectedSlot = null;
			GameManager.Instance.selectedUnit = null;
			GameManager.Instance.uiHeroInfoPanel.gameObject.SetActive(false);
			GameManager.Instance.uiHeroShopPanel.gameObject.SetActive(false);
			GameManager.Instance.Save();
		});

		buttonEquip.onClick.AddListener (() => {
			if(null == GameManager.Instance.selectedUnit)
			{
				return;
			}

			HeroUnit unit = GameManager.Instance.selectedUnit;
			GameManager.Instance.selectedSlot.EquipUnit(unit);

			GameManager.Instance.selectedUnit = null;
			GameManager.Instance.selectedSlot = null;
			GameManager.Instance.uiHeroInfoPanel.gameObject.SetActive(false);
			GameManager.Instance.uiHeroShopPanel.gameObject.SetActive(false);
			GameManager.Instance.Save();
		});

        buttonLevelup.onClick.AddListener (() => {
			if(null == GameManager.Instance.selectedUnit)
			{
				return;
			}
			HeroUnit unit = GameManager.Instance.selectedUnit;
			unit.Upgrade();
			Init();
			contentHeroShop.SetUnit(unit);
			GameManager.Instance.Save();
		});
	}
	public void Init()
    {
		HeroUnit unit = GameManager.Instance.selectedUnit;
		if (null == unit) {
			return;
		}
		unit.Init ();
		textName.text = unit.info.name;
		imageUnit.sprite = unit.info.icon;
		textLevel.text = unit.level.ToString ();

		textAttackPower.gameObject.SetActive (false);
		textAttackSpeed.gameObject.SetActive (false);
		textCritical.gameObject.SetActive (false);
		imageUnit.gameObject.SetActive (true);
		textAttackPower.gameObject.SetActive (true);
		textAttackSpeed.gameObject.SetActive(true);
		textCritical.gameObject.SetActive (true);	
		textAttackPower.text = (Mathf.Round(unit.passiveAttack.data.power * 10.0f) * 0.1f).ToString();
		textAttackSpeed.text = (Mathf.Round(unit.passiveAttack.data.speed * 10.0f) * 0.1f).ToString();

		passiveSkill.Init (unit.passiveAttack);
		activeSkill.Init (unit.activeAttack);
		int upgradeGold = unit.info.upgradePrice * unit.level;
		buttonLevelup.transform.FindChild ("Text").GetComponent<Text> ().text = "Upgrade\n<size=10>(" + upgradeGold.ToString()+ " G)</size>";

		buttonBuy.gameObject.SetActive (false);
		buttonBuy.transform.FindChild ("Text").GetComponent<Text> ().text = "Buy\n<size=10>(" + unit.info.purchasePrice.ToString() + " G)</size>";
		buttonEquip.gameObject.SetActive (false);
		buttonLevelup.gameObject.SetActive (false);

		if (true == unit.purchased) {
			buttonLevelup.gameObject.SetActive (true);
		} else {
			buttonBuy.gameObject.SetActive (true);
		}

		if (true == unit.purchased && (unit.slotIndex != GameManager.Instance.selectedSlot.slotIndex || false == unit.equiped)) {
			buttonEquip.gameObject.SetActive (true);
		}
    }
}
