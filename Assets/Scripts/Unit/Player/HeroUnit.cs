﻿using UnityEngine;
using System.Collections;
public class HeroUnit : Unit {
	[System.Serializable]
	public class Info
	{
		public Sprite icon;
		public string name;
		public string description;
		public int purchasePrice;
		public int upgradePrice; // level * upgradePrice
	}
	public Info info;

	[ReadOnly] public int	level = 1;
	[ReadOnly] public int	slotIndex; 
	[ReadOnly] public bool	purchased;
	[ReadOnly] public bool	equiped;

	private float coolTime;
	public ProgressBar coolTimeBar;
	public TouchEvent touchEvent;

	public override void Start() {
		base.Start ();
		level = 1;
        if (null != touchEvent)
        {
			touchEvent.onTouchDown += (Vector3 position) => {
				if (activeAttack.data.mana > GameManager.Instance.citadel.mana) {
					GameManager.Instance.uiMessageBox.message = "마나가 부족 합니다";
					return;
				}

				if(0 < coolTime)
				{
					return;
				}

				GameManager.Instance.citadel.mana -= (int)activeAttack.data.mana;
                activeAttack.self = this;
                activeAttack.Attack ();
				coolTime = activeAttack.data.cooltime;
			};
			touchEvent.gameObject.SetActive (false);
        }

        if (null != passiveAttack)
        {
            unitAnimation.animationEvents.Add("attack", passiveAttack.Attack);
            passiveAttack.self = this;
            passiveAttack.Upgrade(level);
        }

		if (null != activeAttack) {
            activeAttack.Upgrade (level);
		}
		
		Init ();
	}
	public void Init() {
		if (null != coolTimeBar) {
			coolTimeBar.progress = 1.0f;
			coolTime = 0.0f;
		}
	}
	void Update () {
		EnemyUnit target = null;
		float minDistance = float.MaxValue;

		Transform enemyManager = GameManager.Instance.enemyManager.transform;
		for (int i = 0; i < enemyManager.childCount; i++) {
			EnemyUnit enemy = enemyManager.GetChild (i).GetComponent<EnemyUnit>();
			if (null == enemy) {
				continue;
			}
			float distance = Mathf.Abs(transform.position.x - enemy.transform.position.x);
			if (distance > passiveAttack.data.minRange && distance < passiveAttack.data.maxRange && 0 < enemy.hp && minDistance > distance) {
				target = enemy;
				minDistance = distance;
			}
		}

		if (null != target) {
			unitAnimation.animator.SetTrigger ("attack");
			passiveAttack.target = target;
			unitAnimation.animator.speed = passiveAttack.data.speed;
		} else {
			unitAnimation.animator.SetTrigger ("idle");
			unitAnimation.animator.speed = 1.0f;
		}
		if (0 < coolTime && null != coolTimeBar) {
			coolTime -= Time.deltaTime;
			coolTimeBar.progress = (activeAttack.data.cooltime - coolTime) / activeAttack.data.cooltime;
		}
	}

	public void Upgrade()
	{
		int upgradeGold = info.upgradePrice * level;
		if (upgradeGold >= GameManager.Instance.gold) {
			GameManager.Instance.uiMessageBox.message = "골드가 부족 합니다";
			return;
		}

		level += 1;
		GameManager.Instance.gold -= upgradeGold;
        if (null != passiveAttack)
        {
            passiveAttack.Upgrade(level);
        }
		if (null != activeAttack) {
            activeAttack.Upgrade (level);
		}
	}
}
	