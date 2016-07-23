using UnityEngine;
using System.Collections;

public class UnitSlot : MonoBehaviour {
	public int slotIndex;
	public int sortingOrder;
	public float altitude;
	public Sprite normalSprite;
	public Sprite selectedSprite;
	[HideInInspector]
	public HeroUnit equippedUnit;
	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().sprite = normalSprite;
		sortingOrder = GetComponent<SpriteRenderer> ().sortingOrder;
		transform.FindChild("TouchEvent").GetComponent<TouchEvent>().onTouchDown += (Vector3 position) =>
        {
			GameManager.Instance.uiHeroShopPanel.gameObject.SetActive(true);
			GameManager.Instance.selectedSlot = this;
			foreach(UnitSlot slot in GameManager.Instance.citadel.slots)
			{
				slot.Select(false);
			}
			Select(true);
			if(null != equippedUnit)
			{
				GameManager.Instance.selectedUnit = equippedUnit;
				for(int i=0; i< GameManager.Instance.uiHeroShopPanel.content.childCount; i++)
				{
					ContentHeroShop contentHeroShop = GameManager.Instance.uiHeroShopPanel.content.GetChild(i).GetComponent<ContentHeroShop>();
					if(equippedUnit == contentHeroShop.unit)
					{
						GameManager.Instance.uiHeroInfoPanel.gameObject.SetActive(false);
						GameManager.Instance.uiHeroInfoPanel.contentHeroShop = contentHeroShop;
						GameManager.Instance.uiHeroInfoPanel.gameObject.SetActive(true);
						break;
					}
				}
			}
        };
	}

	public void Select(bool flag)
	{
		if (true == flag) {
			GetComponent<SpriteRenderer> ().enabled = true;
			GetComponent<SpriteRenderer> ().sprite = selectedSprite;
		} else {
			GetComponent<SpriteRenderer> ().sprite = normalSprite;
			if (null != equippedUnit) {
				GetComponent<SpriteRenderer> ().enabled = false;
			}
		}
	}

	public void EquipUnit(HeroUnit unit)
	{
        UnequipUnit();

		if (true == unit.equiped) {
			UnitSlot slot = GameManager.Instance.citadel.slots [unit.slotIndex];
			slot.UnequipUnit ();
		}

		unit.slotIndex = slotIndex;
		unit.equiped = true;
		unit.altitude = altitude;
		unit.transform.position = transform.position;
		unit.gameObject.SetActive(true);
		unit.unitAnimation.spriteRenderer.sortingOrder = sortingOrder + 1;
		unit.transform.SetParent (transform);
		equippedUnit = unit;
		GetComponent<SpriteRenderer> ().enabled = false;
	}

	public void UnequipUnit()
	{
		if (null == equippedUnit) {
			return;
		}

		equippedUnit.equiped = false;
		equippedUnit.gameObject.SetActive (false);
		equippedUnit = null;
		GetComponent<SpriteRenderer> ().enabled = true;
	}
}
