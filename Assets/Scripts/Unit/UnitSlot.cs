using UnityEngine;
using System.Collections;

public class UnitSlot : MonoBehaviour {
	public int slotIndex;
	public int sortingOrder {
		get {
			return GetComponent<SpriteRenderer> ().sortingOrder;
		}
	}
	public float altitude;
	public bool selected {
		set {
			if (true == value) {
				GetComponent<SpriteRenderer> ().enabled = true;
				GetComponent<SpriteRenderer> ().sprite = selectedSprite;
			} else {
				GetComponent<SpriteRenderer> ().sprite = normalSprite;
				if (null != equippedUnit) {
					GetComponent<SpriteRenderer> ().enabled = false;
				}
			}
		}
	}
	public Sprite normalSprite;
	public Sprite selectedSprite;
	public TouchEvent touch;
	public GameObject guide;
	[HideInInspector]
	public HeroUnit equippedUnit;
	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().sprite = normalSprite;
		touch.onTouchDown += (Vector3 position) =>
        {
			GameManager.Instance.uiHeroShopPanel.gameObject.SetActive(true);
			GameManager.Instance.selectedSlot = this;
			foreach(CitadelParts parts in GameManager.Instance.citadel.citadelParts)
			{
				parts.slot.selected = false;
			}
			this.selected = true;
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
		
	public void EquipUnit(HeroUnit unit)
	{
        UnequipUnit();

		if (true == unit.equiped) {
			UnitSlot slot = GameManager.Instance.citadel.citadelParts [unit.slotIndex].slot;
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
		guide.SetActive (false);
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

	public void SetActive(bool flag)
	{
		if (null == equippedUnit) {
			guide.SetActive (flag);
			GetComponent<SpriteRenderer> ().enabled = flag;
		}
		touch.gameObject.SetActive (flag);
	}
}
