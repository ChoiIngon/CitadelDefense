using UnityEngine;
using System.Collections;

public class EnemySkeleton : Enemy {
    public int attackPoint;
    public override void Attack()
    {
        citadel.hp -= attackPoint;
    }
}
