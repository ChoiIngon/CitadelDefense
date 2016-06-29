using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIShopUnitInfo : MonoBehaviour {
	[HideInInspector]
	public BasePlayerUnit unit;

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

	public void SetUnit(BasePlayerUnit unit)
	{
		if (null == unit.state) {
			return;
		}
		this.unit = unit;

		unitIcon.sprite = unit.sprite;
		// skillIcon.sprite = ;
		unitName.text = unit.name;
		if (true == unit.state.equiped) {
			unitEquip.gameObject.SetActive (true);
		} else {
			unitEquip.gameObject.SetActive (false);
		}
		unitLevel.text = "Lv." + unit.state.level.ToString ();

		if (true == unit.state.purchased) {
			unitPrice.gameObject.SetActive (false);
		} else {
			unitPrice.text = unit.price.ToString();
			unitPrice.gameObject.SetActive (true);
		}
	}
}
