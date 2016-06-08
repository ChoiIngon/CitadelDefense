using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class UnitSlot : MonoBehaviour {
    public UnitTurret turret;
	// Use this for initialization
	void Start () {
        gameObject.tag = "UnitSlot";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
