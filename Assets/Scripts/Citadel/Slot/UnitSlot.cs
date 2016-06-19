using UnityEngine;
using System.Collections;

public class UnitSlot : MonoBehaviour {
	public int slotIndex;
	[HideInInspector]
	public BasePlayerUnit unit;
	// Use this for initialization
	void Start () {
        transform.FindChild("TouchEvent").GetComponent<TouchEvent>().onEvent += () =>
        {
            if (GameManager.GameState.Lobby != GameManager.Instance.state)
			{
				return;
			}
			GameManager.Instance.unitShopPanel.gameObject.SetActive(true);
			GameManager.Instance.selectedSlot = this;
        };
	}
}
