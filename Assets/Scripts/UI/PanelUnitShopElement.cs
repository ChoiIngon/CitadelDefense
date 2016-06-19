using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelUnitShopElement : MonoBehaviour {
	public BasePlayerUnit unit;

	public Image unitIcon  { get { return transform.FindChild("ImageUnitIcon").GetComponent<Image>(); } }
	public Image skillIcon { get { return transform.FindChild("ImageSkillIcon").GetComponent<Image>(); } }
	public Text  unitName  { get { return transform.FindChild("TextUnitName").GetComponent<Text>(); } }
	public Text  unitEquip { get { return transform.FindChild("TextUnitEquip").GetComponent<Text>(); } }
	public Text  unitLevel { get { return transform.FindChild("TextUnitLevel").GetComponent<Text>(); } }
	public Text  unitPrice { get { return transform.FindChild("TextUnitPrice").GetComponent<Text>(); } }

	// Use this for initialization
	void Start () {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
			GameManager.Instance.selectedUnit = unit;
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
		unitLevel.text = unit.state.level.ToString ();
		if (true == unit.state.purchased) {
			unitPrice.gameObject.SetActive (true);
		} else {
			unitPrice.gameObject.SetActive (false);
		}
	}
}
