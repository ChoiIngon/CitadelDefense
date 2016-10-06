using UnityEngine;
using System.Collections;

public class EnemyUnit : Unit {
	[System.Serializable]
	public class UpgradeInfo
	{
		public float health;
		public float gold;
	}
    public enum ActionState
    {
		Idle,
        Move,
        Attack,
        Dead
    }

    public string id;
    public ActionState actionState = ActionState.Move;
	public AutoRecoveryInt health;
    public float gold;
    public float exp;    
	public UpgradeInfo upgrade;
    public Vector3 direction;

	public ProgressBar healthBar;

	public override void Start () {
		base.Start ();
		targetTag = "Player";

		health.max = (int)(health.max + upgrade.health * (GameManager.Instance.waveLevel - 1));
		health.value = health.max;
	    
		gold = gold + upgrade.gold * (GameManager.Instance.waveLevel - 1);

        if (null != passiveAttack)
        {
            unitAnimation.animationEvents.Add("attack", passiveAttack.Attack);
            passiveAttack.self = this;
            passiveAttack.Upgrade(GameManager.Instance.waveLevel);
        }

		StartCoroutine (Action ());
    }


	IEnumerator Action()
	{
		actionState = ActionState.Idle;
		while (ActionState.Dead != actionState) {
			//if (0 < health.max) {
			healthBar.progress = (float)health.GetValue () / (float)health.max;
			//}
			unitAnimation.spriteRenderer.sortingOrder = (int)(transform.position.y * -1000);

			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, passiveAttack.data.maxRange, 1 << LayerMask.NameToLayer("Citadel"));
			if(null != hit.collider)
			{
				actionState = ActionState.Attack;
				unitAnimation.animator.SetTrigger("attack");

				if (null != unitMove.buff)
				{
					unitAnimation.animator.speed = unitMove.speed;
				}
				else
				{
					unitAnimation.animator.speed = passiveAttack.data.speed;
				}

				UnitColliderDamage colDamage = hit.collider.GetComponent<UnitColliderDamage>();
				if (null != colDamage)
				{
					passiveAttack.target = colDamage.unit;
				}
				unitMove.enabled = false;
			}
			else
			{
				actionState = ActionState.Move;
				unitAnimation.animator.SetTrigger("move");
				unitAnimation.animator.speed = unitMove.speed;
				unitMove.enabled = true;
				unitMove.Init (transform.position + Vector3.left, altitude);
			}

			yield return new WaitForSeconds(0.1f);
		}

		unitMove.enabled = false;
		unitAnimation.animator.SetTrigger ("dead");
		unitAnimation.animator.speed = 1.0f;
		healthBar.gameObject.SetActive (false);

		int rewardGold = (int)gold + (int)(gold * GameManager.Instance.goldBonus);
		GameManager.Instance.gold += rewardGold;

		GameObject go = new GameObject ();
		go.name = "Effect_GoldReward";
		go.transform.position = transform.position;
		go.AddComponent<UnityStandardAssets.Utility.TimedObjectDestructor> ();
		GameObject effect = GameObject.Instantiate<GameObject>(GameManager.Instance.effectGoldReward);
		effect.transform.position = transform.position;
		effect.GetComponentInChildren<MeshRenderer> ().sortingLayerName = "Effect";
		effect.GetComponentInChildren<TextMesh> ().text = rewardGold.ToString ();
		effect.transform.SetParent (go.transform);

		while (true) {
			AnimatorStateInfo state = unitAnimation.animator.GetCurrentAnimatorStateInfo (0);
			if (1.0f <= state.normalizedTime) {
				DestroyImmediate (gameObject, true);
				break;
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	public override void Damage(int damage)
	{
		if (0 >= health) {
			return;
		}

		health -= damage;
		if (0 >= health) {
			actionState = ActionState.Dead;
		}
	}
}
