using UnityEngine;
using System.Collections;

public class TowerUnit : BasePlayerUnit {
	/*
    [System.Serializable]
	public class LevelupInfo : UnitAttack.AttackData 
	{
		public int baseNeedGold;
		public float needGoldIncreaseRate;
	}
	*/
	//public EnemyUnit target;

    //public UnitAttack.AttackInfo attackInfo;
	//public LevelupInfo levelupInfo;

	public override void Init () {
		base.Init();
        /*
		unitAttack.info = attackInfo;
		unitAttack.self = this;
        InitAttackData();
        */
	}

    void InitAttackData()
    {
		/*
        unitAttack.data.power = unitAttack.info.power + (unitAttack.info.power * levelupInfo.power * (state.level-1));
        unitAttack.data.maxRange = unitAttack.info.maxRange + (unitAttack.info.maxRange * levelupInfo.maxRange * (state.level-1));
		unitAttack.data.minRange = unitAttack.info.minRange + (unitAttack.info.minRange * levelupInfo.minRange * (state.level-1));
        unitAttack.data.speed = unitAttack.info.speed + (unitAttack.info.speed * levelupInfo.speed * (state.level-1));
        */
    }
	// Update is called once per frame
	void Update () {
		/*
		target = null;
		Transform enemyManager = GameManager.Instance.enemyManager.transform;
		for (int i = 0; i < enemyManager.childCount; i++) {
			EnemyUnit enemy = enemyManager.GetChild (i).GetComponent<EnemyUnit>();
			if (null == enemy) {
				continue;
			}
			float distance = Mathf.Abs(transform.position.x - enemy.transform.position.x);
			if (distance > unitAttack.data.minRange && distance < unitAttack.data.maxRange && 0 < enemy.health) {
				target = enemy;
				unitAttack.target = target;
				break;
			}
		}
		/*
		if (null != target) {
			unitAnimation.animator.SetTrigger ("attack");
			unitAnimation.animator.speed = unitAttack.data.speed;
		} else {
			unitAnimation.animator.SetTrigger ("idle");
			unitAnimation.animator.speed = 1.0f;
		}
		*/
	}

	public override void Levelup()
	{
		/*
        int needGold = levelupInfo.baseNeedGold + (int)((state.level - 1) * levelupInfo.baseNeedGold * levelupInfo.needGoldIncreaseRate);
        if (needGold >= GameManager.Instance.gold) {
			GameManager.Instance.messageBox.message = "not enough gold";
			return;
		}

        state.level += 1;
        GameManager.Instance.gold -= needGold;
        InitAttackData();
        */
	}
}
