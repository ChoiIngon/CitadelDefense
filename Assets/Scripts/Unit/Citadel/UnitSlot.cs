using UnityEngine;
using System.Collections;

public class UnitSlot : MonoBehaviour {
	public int slotIndex;
	[HideInInspector]
	public HeroUnit equippedUnit;
	// Use this for initialization
	void Start () {
		transform.FindChild("TouchEvent").GetComponent<TouchEvent>().onTouchDown += (Vector3 position) =>
        {
			GameManager.Instance.panelUnitShop.gameObject.SetActive(true);
			GameManager.Instance.selectedSlot = this;
        };
	}

	public void EquipUnit(HeroUnit unit)
	{
		if (null != equippedUnit) {
			equippedUnit.equiped = false;
			equippedUnit.gameObject.SetActive (false);
		}

		if (true == unit.equiped) {
			UnitSlot slot = GameManager.Instance.slots [unit.slotIndex];
			slot.UnequipUnit ();
		}

		unit.slotIndex = slotIndex;
		unit.equiped = true;
		unit.transform.position = transform.position;
		unit.gameObject.SetActive(true);
		equippedUnit = unit;
	}

	public void UnequipUnit()
	{
		if (null == equippedUnit) {
			return;
		}

		equippedUnit.equiped = false;
		equippedUnit.gameObject.SetActive (false);
		equippedUnit = null;
	}
}
