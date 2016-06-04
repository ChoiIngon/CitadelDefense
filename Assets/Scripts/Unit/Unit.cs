using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Unit : MonoBehaviour {
	public virtual void Damage(int damage) {
	}
}
