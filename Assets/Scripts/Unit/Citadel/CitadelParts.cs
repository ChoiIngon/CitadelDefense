using UnityEngine;
using System.Collections;

public class CitadelParts : MonoBehaviour {
	public int slotIndex;
	public int sortingOrder;
	public float altitude;

	public UnitSlot slot;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().sortingOrder = sortingOrder * 10;
		slot.slotIndex = slotIndex;
		slot.GetComponent<SpriteRenderer> ().sortingOrder = sortingOrder * 10 - 2;
		SpriteRenderer back = transform.FindChild ("Back").GetComponent<SpriteRenderer> ();
		back.sortingOrder = sortingOrder * 10 - 3;
	}
}
