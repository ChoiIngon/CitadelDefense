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

			BasePlayerUnit unit = GameManager.Instance.selectedUnit;
			unit.state.purchased = true;
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

			BasePlayerUnit unit = GameManager.Instance.selectedUnit;
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
			BasePlayerUnit unit = GameManager.Instance.selectedUnit;
            unit.Levelup();
			Init();
			unitInfo.SetUnit(unit);
		});
	}
	public void Init()
    {
		BasePlayerUnit unit = GameManager.Instance.selectedUnit;
		if (null == unit) {
			return;
		}
		textName.text = unit.name;
		textLevel.text = unit.state.level.ToString ();

		imageUnit.sprite = unit.sprite;
		textAttackPower.gameObject.SetActive (false);
		textAttackSpeed.gameObject.SetActive (false);
		textCritical.gameObject.SetActive (false);
		if (unit is TowerUnit) {
			imageUnit.gameObject.SetActive (true);
			textAttackPower.gameObject.SetActive (true);
			textAttackSpeed.gameObject.SetActive(true);
			textCritical.gameObject.SetActive (true);	
			TowerUnit tower = (TowerUnit)unit;
			textAttackPower.text = tower.unitAttack.data.power.ToString();
            textAttackSpeed.text = tower.unitAttack.data.speed.ToString();

			int needGold = tower.levelupInfo.baseNeedGold + (int)((tower.state.level - 1) * tower.levelupInfo.baseNeedGold * tower.levelupInfo.needGoldIncreaseRate);
			buttonLevelup.transform.FindChild ("Text").GetComponent<Text> ().text = "Level Up(" + needGold.ToString () + ")";
		}
		/*
		attackPowerImage.sprite = unit.normalAttackInfo.sprite;
		specialAttackImage.sprite = unit.specialAttackInfo.sprite;
		skillName.text = unit.specialAttackInfo.name;
		*/
		buttonBuy.gameObject.SetActive (false);
		buttonEquip.gameObject.SetActive (false);
		buttonLevelup.gameObject.SetActive (false);

		if (true == unit.state.purchased) {
			buttonLevelup.gameObject.SetActive (true);
		} else {
			buttonBuy.gameObject.SetActive (true);
		}

		if (true == unit.state.purchased && unit.state.index != GameManager.Instance.selectedSlot.slotIndex) {
			buttonEquip.gameObject.SetActive (true);
		}
    }
}
