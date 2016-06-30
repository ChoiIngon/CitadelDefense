using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnitAnimation))]
public class TowerUnit : BasePlayerUnit {
    [System.Serializable]
	public class LevelupInfo : UnitAttack.AttackData 
	{
		public int baseNeedGold;
		public float needGoldIncreaseRate;
	}
	private EnemyUnit target;

    public AnimationClip idleAnimationClip;
    public AnimationClip attackAnimationClip;
	public UnitAttack.AttackInfo attackInfo;
	public LevelupInfo levelupInfo;

	public override void Init () {
		base.Init();
        if (null != idleAnimationClip)
        {
            unitAnimation.ChangeAnimationClip("idle", idleAnimationClip);
        }
        if (null != attackAnimationClip)
        {
            unitAnimation.ChangeAnimationClip("attack", attackAnimationClip);
        }

		unitAnimation.animationEvents.Add ("attack", unitAttack.Attack);

		unitAttack.info = attackInfo;
		unitAttack.self = this;
        InitAttackData();
	}

    void InitAttackData()
    {
        unitAttack.data.power = unitAttack.info.power + (unitAttack.info.power * levelupInfo.power * (state.level-1));
        unitAttack.data.range = unitAttack.info.range + (unitAttack.info.range * levelupInfo.range * (state.level-1));
        unitAttack.data.speed = unitAttack.info.speed + (unitAttack.info.speed * levelupInfo.speed * (state.level-1));
    }
	// Update is called once per frame
	void Update () {
		target = null;
		Transform enemyManager = GameManager.Instance.enemyManager.transform;
		for (int i = 0; i < enemyManager.childCount; i++) {
			EnemyUnit enemy = enemyManager.GetChild (i).GetComponent<EnemyUnit>();
			if (null == enemy) {
				continue;
			}
			float distance = Vector3.Distance (transform.position, enemy.transform.position);
			if (distance < unitAttack.data.range && 0 < enemy.health) {
				target = enemy;
				unitAttack.target = target;
				break;
			}
		}

		if (null != target) {
			unitAnimation.animator.SetTrigger ("attack");
			unitAnimation.animator.speed = unitAttack.data.speed;
		} else {
			unitAnimation.animator.SetTrigger ("idle");
			unitAnimation.animator.speed = 1.0f;
		}
	}

	public override void Levelup()
	{
        int needGold = levelupInfo.baseNeedGold + (int)((state.level - 1) * levelupInfo.baseNeedGold * levelupInfo.needGoldIncreaseRate);
        if (needGold >= GameManager.Instance.gold) {
			throw new System.Exception ("not enough gold");
		}

        state.level += 1;
        GameManager.Instance.gold -= needGold;
        InitAttackData();
	}
}
