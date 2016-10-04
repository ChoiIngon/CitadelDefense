using UnityEngine;
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
	[ReadOnly] public int	slotIndex; 
	public bool	purchased;
	[ReadOnly] public bool	equiped;
	[ReadOnly] public float height;

	private EnemyUnit targetUnit;
	private float coolTime;
	public ProgressBar coolTimeBar;
	public TouchEvent touch;

	public override void Start() {
		base.Start ();
		targetTag = "Enemy";
		targetUnit = null;

		if (null != touch)
        {
			touch.onTouchDown += (Vector3 position) => {
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
			touch.gameObject.SetActive (false);
        }

        if (null != passiveAttack)
        {
            unitAnimation.animationEvents.Add("attack", passiveAttack.Attack);
            passiveAttack.self = this;
        }
			
		Init ();
	}
	public void Init() {
		if (null != coolTimeBar) {
			coolTimeBar.progress = 1.0f;
			coolTime = 0.0f;
		}
		if (null != passiveAttack) {
			passiveAttack.Upgrade(level);
		}
		if (null != activeAttack) {
			activeAttack.Upgrade(level);
		}

		unitAnimation.animator.SetTrigger ("idle");
		unitAnimation.animator.speed = 1.0f;
	}
	public void SetActive(bool flag)
	{
		if (null != touch) 
		{
			touch.gameObject.SetActive (flag);
		}
	}
	void Update () {
		if (null == targetUnit || 0 >= targetUnit.hp) {
			float minDistance = float.MaxValue;

			Transform enemyManager = GameManager.Instance.enemyManager.transform;
			for (int i = 0; i < enemyManager.childCount; i++) {
				EnemyUnit enemy = enemyManager.GetChild (i).GetComponent<EnemyUnit> ();
				if (null == enemy) {
					continue;
				}
				float distance = Mathf.Abs (transform.position.x - enemy.transform.position.x);
				if (distance > passiveAttack.data.minRange && distance < passiveAttack.data.maxRange && 0 < enemy.hp && minDistance > distance) {
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
	