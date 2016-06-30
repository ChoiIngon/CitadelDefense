﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class UnitColliderDamage : MonoBehaviour {
	public Unit unit;

    void Start()
    {
        unit = GetComponentInParent<Unit>();
    }
}
