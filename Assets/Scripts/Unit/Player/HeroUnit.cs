using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
#if UNITY_EDITOR
	[ReadOnly] public int	level = 1;
	[ReadOnly] public int	slotIndex; 
	[ReadOnly] public bool	purchased;
	[ReadOnly] public bool	equiped;
#else
	public int	level;
	public int	slotIndex; 
	public bool	purchased;
	public bool	equiped;
#endif
	private float coolTime;
	public ProgressBar coolTimeBar;
	public UnitAnimation unitAnimation;
	public UnitAttack normalAttack;
    public UnitAttack specialAttack;
	public TouchEvent touchEvent;

	void Start() {
		level = 1;
        if (null != touchEvent)
        {
			touchEvent.onTouchDown += (Vector3 position) => {
				if (GameManager.GameState.Play != GameManager.Instance.state) {
					return;
				}

				if (specialAttack.data.mana > GameManager.Instance.citadel.mp) {
					GameManager.Instance.messageBox.message = "마나가 부족 합니다";
					return;
				}

				if(0 < coolTime)
				{
					return;
				}

				GameManager.Instance.citadel.mp -= (int)specialAttack.data.mana;
				specialAttack.self = this;
				specialAttack.Attack ();
				coolTime = specialAttack.data.cooltime;
			};
			touchEvent.gameObject.SetActive (false);
        }
		normalAttack.Upgrade (level);
		if (null != specialAttack) {
			specialAttack.Upgrade (level);
		}
		unitAnimation.animationEvents.Add ("attack", normalAttack.Attack);
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
			if (distance > normalAttack.data.minRange && distance < normalAttack.data.maxRange && 0 < enemy.health && minDistance > distance) {
				target = enemy;
				minDistance = distance;
			}
		}

		if (null != target) {
			unitAnimation.animator.SetTrigger ("attack");
			normalAttack.target = target;
			unitAnimation.animator.speed = normalAttack.data.speed;
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
			GameManager.Instance.messageBox.message = "골드가 부족 합니다";
			return;
		}

		level += 1;
		GameManager.Instance.gold -= upgradeGold;
		normalAttack.Upgrade (level);
		if (null != specialAttack) {
			specialAttack.Upgrade (level);
		}
	}
}

#if UNITY_EDITOR
public class ReadOnlyAttribute : PropertyAttribute
{

}

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
	public override float GetPropertyHeight(SerializedProperty property,
		GUIContent label)
	{
		return EditorGUI.GetPropertyHeight(property, label, true);
	}

	public override void OnGUI(Rect position,
		SerializedProperty property,
		GUIContent label)
	{
		GUI.enabled = false;
		EditorGUI.PropertyField(position, property, label, true);
		GUI.enabled = true;
	}
}
#endif