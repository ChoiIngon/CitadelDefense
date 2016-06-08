using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Unit : MonoBehaviour {
	protected SpriteRenderer unitSprite;
	protected UnitAnimation unitAnimation;
	protected UnitAttack unitAttack;
	protected UnitColliderDamage unitCollderDamage;

	protected void Start()
	{
		unitSprite = GetComponent<SpriteRenderer> ();
		if (null == unitSprite) {
			throw new System.Exception ("fail to load \'SpriteRenderer\'");
		}
		//unitSprite.sortingLayerName = "Unit";
		unitAnimation = GetComponent<UnitAnimation> ();
		if (null != unitAnimation) {
			unitAnimation.Init ();
		}


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
