using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class UnitColliderDot : MonoBehaviour
{
    public Dictionary<Unit, Unit> targets;
    public string targetUnitTag;
    public float power;
    public float interval;
    private float deltaTime;
    // Use this for initialization
    protected void Start()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;

        Collider2D collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
        targets = new Dictionary<Unit, Unit>();
        deltaTime = 0.0f;
    }

    void OnEnable()
    {
        deltaTime = 0.0f;
    }
    
    void OnTriggerStay2D(Collider2D col)
    {
        deltaTime += Time.deltaTime;
        if(deltaTime < interval)
        {
            return;
        }
        deltaTime = 0.0f;
        if (targetUnitTag == col.gameObject.tag)
        {
            UnitColliderDamage colliderDamage = col.gameObject.GetComponent<UnitColliderDamage>();
            if (null != colliderDamage)
            {
                colliderDamage.unit.Damage((int)power);
            }
        }
    }
}
