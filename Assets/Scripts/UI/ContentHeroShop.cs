﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContentHeroShop : MonoBehaviour {
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
			GameManager.Instance.uiHeroInfoPanel.contentHeroShop = this;
			GameManager.Instance.uiHeroInfoPanel.gameObject.SetActive(true);
        });
	}

	public void SetUnit(HeroUnit unit)
	{
		this.unit = unit;

		unitIcon.sprite = unit.info.icon;
		unitName.text = unit.info.name;
		unitDescription.text = unit.info.description;
		if (null != unit.activeAttack) {
			skillIcon.sprite = unit.activeAttack.info.icon;
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
			unitPrice.text = "구매 완료";
		} else {
			unitPrice.text = unit.info.purchasePrice.ToString() + " G";
		}
	}
}
