using UnityEngine;
using System.Collections;

public class UnitSlot : MonoBehaviour {
	public int slotIndex;
	public int sortingOrder;
	[HideInInspector]
	public HeroUnit equippedUnit;
	// Use this for initialization
	void Start () {
		sortingOrder = GetComponent<SpriteRenderer> ().sortingOrder;
		transform.FindChild("TouchEvent").GetComponent<TouchEvent>().onTouchDown += (Vector3 position) =>
        {
			GameManager.Instance.panelUnitShop.gameObject.SetActive(true);
			GameManager.Instance.selectedSlot = this;
        };
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
		unit.transform.position = transform.position;
		unit.gameObject.SetActive(true);
		unit.unitAnimation.spriteRenderer.sortingOrder = sortingOrder + 1;
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
