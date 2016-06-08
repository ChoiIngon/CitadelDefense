using UnityEngine;
using System.Collections;

public class HeroSlot : MonoBehaviour {
    public UnitTurret turret;
    public GameObject heroPanel;
	// Use this for initialization
	void Start () {
        transform.FindChild("TouchEvent").GetComponent<TouchEvent>().onEvent += () =>
        {
            Debug.Log(name + " is touched");
        };
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
