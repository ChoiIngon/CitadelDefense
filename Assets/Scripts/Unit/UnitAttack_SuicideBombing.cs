using UnityEngine;
using System.Collections;

public class UnitAttack_SuicideBombing : UnitAttack {
    public UnitHit hitPrefab;
	
    public override void Attack()
    {
        UnitHit hit = GameObject.Instantiate<UnitHit>(hitPrefab);
        hit.attack = this;
        hit.transform.position = transform.position;
        Destroy(self.gameObject);
    }
}
