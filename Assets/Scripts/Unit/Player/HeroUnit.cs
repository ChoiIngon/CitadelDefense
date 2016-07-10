using UnityEngine;
using System.Collections;

public class HeroUnit : TowerUnit {
	public ProgressBar coolTimeBar;
    public UnitAttack specialAttack;
    public TouchEvent touchEvent;
	// Use this for initialization
	public override void Init() {
		base.Init ();
        if (null != touchEvent)
        {
            touchEvent.onTouchDown += OnTouchDown;
        }
	}

	public void OnTouchDown(Vector3 position)
    {
        if (GameManager.GameState.Play != GameManager.Instance.state)
        {
            return;
        }

		if (specialAttack.data.mana > GameManager.Instance.citadel.mp) {
			return;
		}

		GameManager.Instance.citadel.mp -= (int)specialAttack.data.mana;
        specialAttack.data.power = unitAttack.data.power * 1.5f;
        specialAttack.self = this;
		specialAttack.Attack ();
    }
}
