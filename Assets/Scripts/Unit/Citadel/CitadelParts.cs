using UnityEngine;
using System.Collections;

public class CitadelParts : MonoBehaviour {
	public int slotIndex;
	public int sortingOrder;
	public float altitude;

	public SpriteRenderer front;
	public SpriteRenderer back;
	public UnitSlot slot;
	[System.Serializable]
	public class SaveData {
		public bool active;
	}

	public void Init()
	{
		front.sortingOrder = sortingOrder * 10;
		slot.slotIndex = slotIndex;
		slot.GetComponent<SpriteRenderer> ().sortingOrder = sortingOrder * 10 - 2;
		back.sortingOrder = sortingOrder * 10 - 3;
	}
}
