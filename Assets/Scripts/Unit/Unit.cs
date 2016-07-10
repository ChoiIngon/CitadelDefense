using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Unit : MonoBehaviour {
	public Sprite sprite;

	public UnitAnimation unitAnimation;
	public UnitAttack unitAttack;
	protected UnitColliderDamage unitCollderDamage;

	public virtual void Init()
	{
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		if (null == renderer) {
			throw new System.Exception ("fail to load \'SpriteRenderer\'");
		}
		sprite = renderer.sprite;
		//unitSprite.sortingLayerName = "Unit";
		unitAnimation = GetComponent<UnitAnimation> ();

		{
			Transform tr = transform.FindChild ("UnitAttack");
			if (null != tr) {
				unitAttack = tr.GetComponent<UnitAttack> ();
				if (null == unitAttack) {
					throw new System.Exception ("fail to load \'UnitAttack\'");
				}
			}
		}

		{
			Transform tr = transform.FindChild ("UnitColliderDamage");
			if (null != tr) {
				unitCollderDamage = tr.GetComponent<UnitColliderDamage> ();
				if (null == unitCollderDamage) {
					throw new System.Exception ("fail to load \'UnitColliderDamage\'");
				}
				unitCollderDamage.unit = this;
			}
		}
	}
	public virtual void Damage(int damage) {}
}
