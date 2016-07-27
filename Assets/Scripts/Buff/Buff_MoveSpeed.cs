using UnityEngine;
using System.Collections;

public class Buff_MoveSpeed : Buff {
	private UnitMove move;
	public float time;
	public float value;
	private float deltaTime;
    // Use this for initialization
    public override void Start () {
        base.Start();
        if(null == unit)
        {
            return;
        }
		deltaTime = 0.0f;
		move = GetComponentInParent<UnitMove> ();
		if (null == move) {
			return;
		}
		if (null != move.buff) {
			return;
		}
		unit.unitAnimation.spriteRenderer.color = Color.blue;
		move.buff += MoveSpeed;
	}

	void MoveSpeed(ref float ret, float original)
	{
		ret += original * value;
	}
	// Update is called once per frame
	void Update () {
		if (null != move) {
			deltaTime += Time.deltaTime;
			if (deltaTime >= time) {
                unit.unitAnimation.spriteRenderer.color = Color.white;
				Destroy (gameObject);
				move.buff -= MoveSpeed;
			}		
		}
	}
}
