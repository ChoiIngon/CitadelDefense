using UnityEngine;
using System.Collections;

public class HeroSlot : MonoBehaviour {
    public UnitHero hero;
    public GameObject heroPanel;
	// Use this for initialization
	void Start () {
        transform.FindChild("TouchEvent").GetComponent<TouchEvent>().onEvent += () =>
        {
			if(GameManager.State.Lobby != GameManager.Instance.state)
			{
				return;
			}
			heroPanel.SetActive(true);
        };
	}
}
