using UnityEngine;
using System.Collections;

public class UnitAttack_Meteor : UnitAttack {
    public Missile missilePrefab;
    public int missileCount;
    private float deltaTime;

    void Start()
    {
        
        
        Attack();
    }

    public override void Attack()
	{
		deltaTime = 0.0f;
        StartCoroutine(Meteor());
    }

    IEnumerator Meteor()
    {
        for (int i = 0; i < missileCount; i++)
        {
            deltaTime = 0.0f;
            float interval = Random.Range(0.1f, 0.2f);
            while(deltaTime < interval)
            {
                deltaTime += Time.deltaTime;
                yield return null;
            }
            Vector3 endPosition = new Vector3(Random.Range(6.5f, 9.5f), Random.Range(1.0f, 6.0f), 0.0f);
            Vector3 startPosition = new Vector3(endPosition.x - 4.0f, endPosition.y + 7.0f, endPosition.z);
            Missile missile = Object.Instantiate<Missile>(missilePrefab);
            missile.Init(startPosition, endPosition, data.power);
        }
    }
}
