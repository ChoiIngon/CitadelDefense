﻿using UnityEngine;
using System.Collections;
public class HeroUnit : Unit {
	[System.Serializable]
	public class Info
	{
		public Sprite icon;
		public string id;
		public string name;
		public string description;
		public int purchasePrice;
		public int upgradePrice; // level * upgradePrice
	}

	[System.Serializable]
	public class SaveData
	{
		public string id;
		public int level;
		public int slotIndex;
		public bool purchased;
		public bool equiped;
	}
	public Info info;

	public int	level;
	public bool	purchased;
	[ReadOnly] public int	slotIndex; 
	[ReadOnly] public bool isActive;
	[ReadOnly] public bool	equiped;
	[ReadOnly] public float height;

	private EnemyUnit targetUnit;
	private float coolTime;
	public ProgressBar coolTimeBar;
	public TouchEvent touch;

	public override void Start() {
        if (null == touch)
        {
            throw new System.Exception("hero touch event is null");
        }

        if (null == passiveAttack)
        {
            throw new System.Exception("hero passive attack data is null");
        }

        base.Start ();

        touch.gameObject.SetActive(false);
        touch.onTouchDown += (Vector3 position) => {
            if (activeAttack.data.mana > GameManager.Instance.citadel.mana)
            {
                GameManager.Instance.uiMessageBox.message = "마나가 부족 합니다";
                return;
            }

            if (0 < coolTime)
            {
                return;
            }

            GameManager.Instance.citadel.mana -= (int)activeAttack.data.mana;
            activeAttack.self = this;
            activeAttack.Attack();
            coolTime = activeAttack.data.cooltime;

            Util.EventSystem.Publish(EventID.ManaChanged, GameManager.Instance.citadel.mana);
        };
        
        targetTag = "Enemy";
		targetUnit = null;

        unitAnimation.animationEvents.Add("attack", PassiveAttack);
        passiveAttack.self = this;
			
		Init ();
	}

	private void PassiveAttack() {
		if (true == isActive) {
			passiveAttack.Attack ();
		}
	}

	public void Init() {
		if (null != coolTimeBar) {
			coolTimeBar.progress = 1.0f;
			coolTime = 0.0f;
		}
		if (null != passiveAttack) {
			passiveAttack.Init ();
			passiveAttack.Upgrade(level);
		}
		if (null != activeAttack) {
			activeAttack.Init ();
			activeAttack.Upgrade(level);
		}
	}
	public void SetActive(bool flag)
	{
		if (null != touch) 
		{
			touch.gameObject.SetActive (flag);
		}
		isActive = flag;
	}
	void Update () {
		if (null == targetUnit || 0 >= targetUnit.health) {
			float minDistance = float.MaxValue;

			Transform enemyManager = GameManager.Instance.enemyManager.transform;
			for (int i = 0; i < enemyManager.childCount; i++) {
				EnemyUnit enemy = enemyManager.GetChild (i).GetComponent<EnemyUnit> ();
				if (null == enemy) {
					continue;
				}
				float distance = Mathf.Abs (transform.position.x - enemy.transform.position.x);
				if (distance < minDistance &&
					distance > passiveAttack.data.minRange && 
					distance < passiveAttack.data.maxRange &&
					0 < enemy.health
				) {
					targetUnit = enemy;
					minDistance = distance;
				}
			}
		}
		if (null != targetUnit) {
			unitAnimation.animator.SetTrigger ("attack");
			passiveAttack.target = targetUnit;
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
	