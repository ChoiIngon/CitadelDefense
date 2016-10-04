using UnityEngine;
using System.Collections;

public class UnitAttack_SuicideBombing : UnitAttack {
    public override void Attack()
    {
		Hit(transform.position);
        Destroy(self.gameObject);
    }
}
