using UnityEngine;
using System.Collections;

public class UnitHit : MonoBehaviour {
	public UnitAttack attack;
    public bool shakeCamera;
    public Rect hitRange;
    public int hitCount;
    public enum HitRangeType
    {
        SingleTarget,
        Rectangle,
        Circle,
        Ellipse
    }

    public virtual void Start()
    {
        if (true == shakeCamera)
        {
            Hashtable ht = new Hashtable();
            ht.Add("x", 0.1f);
            ht.Add("y", 0.1f);
            ht.Add("time", 0.3f);
            iTween.ShakePosition(Camera.main.gameObject, ht);
        }
    }

    public virtual void Init(Vector3 position, float power)
	{
		GetComponent<UnitColliderAttack> ().attack = attack;
		transform.position = position;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        /*
        if (attack.targetTag == col.gameObject.tag && 0 < hitCount)
        {
            UnitColliderDamage colliderDamage = col.gameObject.GetComponent<UnitColliderDamage>();
            if (null != colliderDamage)
            {
                hitCount -= 1;
                attack.Damage(colliderDamage.unit);
            }
        }
        */
    }
}
