using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAttack_Range : UnitAttack
{
    public enum BulletMoveType
    {
        Curve,
        Stright
    }

    public Sprite bulletSprite;
    public BulletMoveType bulletMove;
    
    [HideInInspector]
    public List<UnitMove> unitMoves = new List<UnitMove>();

    public override void Attack()
    {
        float distance = Vector3.Distance(self.transform.position, target.transform.position);
        GameObject bullet = new GameObject();
        SpriteRenderer spriteRenderder = bullet.AddComponent<SpriteRenderer>();
        spriteRenderder.sortingLayerName = "Unit";
        spriteRenderder.sprite = bulletSprite;

        UnitMove unitMove = null;
        if(BulletMoveType.Curve == bulletMove)
        {
            UnitMove_SinCurve sinCurveMove = bullet.AddComponent<UnitMove_SinCurve>();
            sinCurveMove.Init(self.transform.position, target.transform.position, distance / 4, 5.0f);
            unitMove = sinCurveMove;
        }
        else if(BulletMoveType.Stright == bulletMove)
        {
            UnitMove_Stright strightMove = bullet.AddComponent<UnitMove_Stright>();
            strightMove.Init(self.transform.position, target.transform.position, 5.0f);
            unitMove = strightMove;
        }
        unitMoves.Add(unitMove);
    }

    void Update()
    {
        List<UnitMove> completeMove = new List<UnitMove>();
        foreach (UnitMove unitMove in unitMoves)
        {
            if (1.0f <= unitMove.interpolate)
            {
                completeMove.Add(unitMove);
                Effect go = GameObject.Instantiate<Effect>(info.effect);
                UnitColliderAttack col = go.GetComponent<UnitColliderAttack>();
                col.attackPower = data.power;
                go.transform.position = unitMove.end;
            }
        }

        foreach (UnitMove unitMove in completeMove)
        {
            unitMoves.Remove(unitMove);
            DestroyImmediate(unitMove.gameObject, true);
        }
    }
}
