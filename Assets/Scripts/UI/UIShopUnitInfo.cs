using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIShopUnitInfo : MonoBehaviour {
	[HideInInspector]
	public HeroUnit unit;

	public Image unitIcon;
	public Image skillIcon;
	public Text unitName;
	public Text unitEquip;
	public Text unitLevel;
	public Text unitPrice;
	public Text unitDescription;
	// Use this for initialization
	void Start () {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
			GameManager.Instance.selectedUnit = unit;
			GameManager.Instance.unitInfoPanel.unitInfo = this;
			GameManager.Instance.unitInfoPanel.gameObject.SetActive(true);
        });
	}

	public void SetUnit(HeroUnit unit)
	{
		this.unit = unit;

		unitIcon.sprite = unit.info.icon;
		unitName.text = unit.info.name;
		unitDescription.text = unit.info.description;
		if (null != unit.specialAttack) {
			skillIcon.sprite = unit.specialAttack.info.icon;
		} else {
			skillIcon.gameObject.SetActive (false);
		}

		if (true == unit.equiped) {
			unitEquip.gameObject.SetActive (true);
		} else {
			unitEquip.gameObject.SetActive (false);
		}
		unitLevel.text = "Lv." + unit.level.ToString ();

		if (true == unit.purchased) {
			unitPrice.gameObject.SetActive (false);
		} else {
			unitPrice.text = unit.info.purchasePrice.ToString();
			unitPrice.gameObject.SetActive (true);
		}
	}
}
