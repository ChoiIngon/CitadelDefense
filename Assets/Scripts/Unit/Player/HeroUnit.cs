using UnityEngine;
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
	public UnitAttack passiveAttack;
    public UnitAttack specialAttack;
	public TouchEvent touchEvent;

	public virtual void Start() {
		base.Start ();
		level = 1;
        if (null != touchEvent)
        {
			touchEvent.onTouchDown += (Vector3 position) => {
				if (specialAttack.data.mana > GameManager.Instance.citadel.mana) {
					GameManager.Instance.uiMessageBox.message = "마나가 부족 합니다";
					return;
				}

				if(0 < coolTime)
				{
					return;
				}

				GameManager.Instance.citadel.mana -= (int)specialAttack.data.mana;
				specialAttack.self = this;
				specialAttack.Attack ();
				coolTime = specialAttack.data.cooltime;
			};
			touchEvent.gameObject.SetActive (false);
        }
		passiveAttack.Upgrade (level);
		if (null != specialAttack) {
			specialAttack.Upgrade (level);
		}
		unitAnimation.animationEvents.Add ("attack", passiveAttack.Attack);
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
			coolTimeBar.progress = (specialAttack.data.cooltime - coolTime) / specialAttack.data.cooltime;
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
		passiveAttack.Upgrade (level);
		if (null != specialAttack) {
			specialAttack.Upgrade (level);
		}
	}
}
	