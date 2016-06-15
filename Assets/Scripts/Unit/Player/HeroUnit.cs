using UnityEngine;
using System.Collections;

public class HeroUnit : BasePlayerUnit {
	public UnitAttack.Info normalAttackInfo;
	public UnitAttack.Info specialAttackInfo;
	public ProgressBar coolTimeBar;
	protected UnitAttack normalAttack;
	protected UnitAttack specialAttack;
	// Use this for initialization
	void Start () {
		base.Start ();
		{
			Transform tr = transform.FindChild ("SpecialAttack");
			if (null != tr) {
				specialAttack = tr.GetComponent<UnitAttack> ();
				if (null != specialAttack) {
					specialAttack.info = specialAttackInfo;
				}
			}
		}
		/*
		transform.FindChild("TouchEvent").GetComponent<TouchEvent>().onEvent += () =>
		{

		};
		*/ 
	}

	public override void ShowInfo(HeroInfoPanel panel)
	{
		panel.heroName.text = name;
		panel.heroLevel.text = level.ToString ();

		panel.attackPowerImage.sprite = normalAttackInfo.sprite;
		panel.specialAttackImage.sprite = specialAttackInfo.sprite;
		panel.skillName.text = specialAttackInfo.name;
		panel.skillImage.sprite = specialAttackInfo.sprite;
		panel.skillDescription.text = specialAttackInfo.description;

		panel.buyButton.gameObject.SetActive (false);
		panel.equipButton.gameObject.SetActive (false);
		panel.levelUpButton.gameObject.SetActive (false);
		if (false == purchased) {
			panel.buyButton.gameObject.SetActive (true);
		} else {
			panel.levelUpButton.gameObject.SetActive (true);
			if (slotIndex != GameManager.Instance.selectedSlot.slotIndex) {
				panel.equipButton.gameObject.SetActive (true);
			}
		}
	}
}
