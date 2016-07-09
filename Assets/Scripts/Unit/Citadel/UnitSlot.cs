using UnityEngine;
using System.Collections;

public class UnitSlot : MonoBehaviour {
	public int slotIndex;
	[HideInInspector]
	public BasePlayerUnit equippedUnit;
	// Use this for initialization
	void Start () {
		transform.FindChild("TouchEvent").GetComponent<TouchEvent>().onTouchDown += (Vector3 position) =>
        {
			GameManager.Instance.panelUnitShop.gameObject.SetActive(true);
			GameManager.Instance.selectedSlot = this;
        };
	}

	public void EquipUnit(BasePlayerUnit unit)
	{
		if (null != equippedUnit) {
			equippedUnit.state.equiped = false;
			equippedUnit.gameObject.SetActive (false);
		}

		if (true == unit.state.equiped) {
			UnitSlot slot = GameManager.Instance.slots [unit.state.index];
			slot.UnequipUnit ();
		}

		unit.state.index = slotIndex;
		unit.state.equiped = true;
		unit.transform.position = transform.position;
		unit.gameObject.SetActive(true);

		equippedUnit = unit;
	}

	public void UnequipUnit()
	{
		if (null == equippedUnit) {
			return;
		}

		equippedUnit.state.equiped = false;
		equippedUnit.gameObject.SetActive (false);
		equippedUnit = null;
	}
}
