using UnityEngine;
using System.Collections;

public class HeroSlot : MonoBehaviour {
	public int slotIndex;
	public UnitHero unit;
	// Use this for initialization
	void Start () {
        transform.FindChild("TouchEvent").GetComponent<TouchEvent>().onEvent += () =>
        {
            if (GameManager.GameState.Lobby != GameManager.Instance.state)
			{
				return;
			}
			GameManager.Instance.heroPanel.gameObject.SetActive(true);
			GameManager.Instance.selectedSlot = this;
        };
	}
}
