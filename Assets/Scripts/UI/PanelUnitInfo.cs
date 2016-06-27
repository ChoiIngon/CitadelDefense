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

	[HideInInspector]
	public UIShopUnitInfo element;
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
			if(null != GameManager.Instance.selectedSlot.unit)
			{
				BasePlayerUnit selectedUnit = GameManager.Instance.selectedSlot.unit;
				selectedUnit.state.equiped = false;
				selectedUnit.gameObject.SetActive(false);
				GameManager.Instance.selectedSlot.unit = null;
			}

			BasePlayerUnit unit = GameManager.Instance.selectedUnit;
			unit.state.index = GameManager.Instance.selectedSlot.slotIndex;
			unit.state.equiped = true;
			unit.state.purchased = true;
			unit.transform.position = GameManager.Instance.selectedSlot.transform.position;
			unit.gameObject.SetActive(true);

			GameManager.Instance.selectedSlot = null;
			GameManager.Instance.selectedUnit = null;
			GameManager.Instance.unitInfoPanel.gameObject.SetActive(false);
			GameManager.Instance.unitShopPanel.gameObject.SetActive(false);
		});

		buttonEquip.onClick.AddListener (() => {
			if(null == GameManager.Instance.selectedUnit)
			{
				return;
			}

			if(null != GameManager.Instance.selectedSlot.unit)
			{
				BasePlayerUnit selectedUnit = GameManager.Instance.selectedSlot.unit;
				selectedUnit.state.equiped = false;
				selectedUnit.gameObject.SetActive(false);
				GameManager.Instance.selectedSlot.unit = null;
			}

			BasePlayerUnit unit = GameManager.Instance.selectedUnit;
			unit.state.index = GameManager.Instance.selectedSlot.slotIndex;
			unit.transform.position = GameManager.Instance.selectedSlot.transform.position;
			unit.gameObject.SetActive(true);

			GameManager.Instance.selectedSlot.unit = unit;

			GameManager.Instance.selectedUnit = null;
			GameManager.Instance.selectedSlot = null;
			GameManager.Instance.unitInfoPanel.gameObject.SetActive(false);
			GameManager.Instance.unitShopPanel.gameObject.SetActive(false);
		});


		buttonLevelup.onClick.AddListener (() => {
			if(null == GameManager.Instance.selectedUnit)
			{
				return;
			}
			BasePlayerUnit unit = GameManager.Instance.selectedUnit;
			unit.state.level += 1;
			Init();
			element.SetUnit(unit);
			Debug.Log("hero unit level:" + unit.state.level);
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
			textAttackPower.text = tower.attackInfo.power.ToString();
            textAttackSpeed.text = tower.attackInfo.speed.ToString();
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
