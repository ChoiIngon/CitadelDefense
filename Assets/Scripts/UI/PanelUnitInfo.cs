using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelUnitInfo : MonoBehaviour {
	const string defaultChildPath = "Background/";
	// Use this for initialization
	public Button 	buttonBuy { get { return transform.FindChild(defaultChildPath + "ButtonBuy").GetComponent<Button>(); }}
	public Button 	buttonEquip { get { return transform.FindChild(defaultChildPath + "ButtonEquip").GetComponent<Button>(); } }
	public Button 	buttonLevelup { get { return transform.FindChild(defaultChildPath + "ButtonLevelup").GetComponent<Button>(); } }
	public Image  	imageUnitIcon { get { return transform.FindChild(defaultChildPath + "ImageUnitIcon").GetComponent<Image>(); }}
	public Text 	textUnitName { get { return transform.FindChild(defaultChildPath + "TextUnitName").GetComponent<Text>(); } }
	public Text 	textUnitLevel { get { return transform.FindChild(defaultChildPath + "TextUnitLevel").GetComponent<Text>(); } }

	public Image 	imageAttackIcon { get { return transform.FindChild(defaultChildPath + "ImageAttackIcon").GetComponent<Image>(); } }
	public Text 	textAttackPower { get { return transform.FindChild(defaultChildPath + "TextAttackPower").GetComponent<Text>(); } }

	public Image 	imageSkillIcon { get { return transform.FindChild(defaultChildPath + "SkillInfoPanel/ImageSkillIcon").GetComponent<Image>(); } }
	public Text 	textSkillName { get { return transform.FindChild(defaultChildPath + "SkillInfoPanel/TextSkillName").GetComponent<Text>(); } }
	public Text 	textSkillDescription { get { return transform.FindChild(defaultChildPath + "SkillInfoPanel/TextSkillDescription").GetComponent<Text>(); } }

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
			Debug.Log("hero unit level:" + unit.state.level);
		});
	}
	public void Init()
    {
		BasePlayerUnit unit = GameManager.Instance.selectedUnit;
		if (null == unit) {
			return;
		}
		textUnitName.text = unit.name;
		textUnitLevel.text = unit.state.level.ToString ();
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

		if (unit.state.index != GameManager.Instance.selectedSlot.slotIndex) {
			buttonEquip.gameObject.SetActive (true);
		}
    }
}
