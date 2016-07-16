using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelUnitInfo : MonoBehaviour {
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

	public UIShopUnitInfo unitInfo;
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
				GameManager.Instance.messageBox.message = "골드가 부족 합니다";
				return;
			}
			GameManager.Instance.gold -= unit.info.purchasePrice;
			unit.purchased = true;
			GameManager.Instance.selectedSlot.EquipUnit(unit);

			GameManager.Instance.selectedSlot = null;
			GameManager.Instance.selectedUnit = null;
			GameManager.Instance.unitInfoPanel.gameObject.SetActive(false);
			GameManager.Instance.panelUnitShop.gameObject.SetActive(false);
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
			GameManager.Instance.unitInfoPanel.gameObject.SetActive(false);
			GameManager.Instance.panelUnitShop.gameObject.SetActive(false);
		});

        buttonLevelup.onClick.AddListener (() => {
			if(null == GameManager.Instance.selectedUnit)
			{
				return;
			}
			HeroUnit unit = GameManager.Instance.selectedUnit;
			unit.Upgrade();
			Init();
			unitInfo.SetUnit(unit);
		});
	}
	public void Init()
    {
		HeroUnit unit = GameManager.Instance.selectedUnit;
		if (null == unit) {
			return;
		}
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
		textAttackPower.text = unit.normalAttack.data.power.ToString();
		textAttackSpeed.text = unit.normalAttack.data.speed.ToString();

		int upgradeGold = unit.info.upgradePrice * unit.level;
		buttonLevelup.transform.FindChild ("Text").GetComponent<Text> ().text = "Level Up(" + upgradeGold.ToString () + ")";

		//attackPowerImage.sprite = unit.normalAttack.info.icon;
		//specialAttackImage.sprite = unit.specialAttack.info.icon;
		//skillName.text = unit.specialAttack.info.name;

		buttonBuy.gameObject.SetActive (false);
		buttonBuy.transform.FindChild ("Text").GetComponent<Text> ().text = unit.info.purchasePrice.ToString();
		buttonEquip.gameObject.SetActive (false);
		buttonLevelup.gameObject.SetActive (false);

		if (true == unit.purchased) {
			buttonLevelup.gameObject.SetActive (true);
		} else {
			buttonBuy.gameObject.SetActive (true);
		}

		if (true == unit.purchased && unit.slotIndex != GameManager.Instance.selectedSlot.slotIndex) {
			buttonEquip.gameObject.SetActive (true);
		}
    }
}
