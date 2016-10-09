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

			RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.left, passiveAttack.data.maxRange, 1 << LayerMask.NameToLayer("Citadel"));
			if(null != ray.collider)
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

				UnitColliderDamage colDamage = ray.collider.GetComponent<UnitColliderDamage>();
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

		GameObject effect = GameObject.Instantiate<GameObject> (GameManager.Instance.effectGoldReward);
		effect.transform.position = transform.position;
		Transform text = effect.transform.Find ("Animation/Text");
		text.GetComponent<MeshRenderer>().sortingLayerName = "Effect";
		text.GetComponent<MeshRenderer>().sortingOrder = 1;
		text.GetComponent<TextMesh> ().text = rewardGold.ToString ();

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
		/*
		GameObject effect = GameObject.Instantiate<GameObject> (GameManager.Instance.effectDamage);
		effect.transform.position = new Vector3(
			hitBox.bounds.center.x, 
			hitBox.bounds.center.y + hitBox.bounds.size.y / 2,
			transform.position.z
		);

		Transform text = effect.transform.Find ("Animation/Text");
		text.GetComponent<MeshRenderer>().sortingLayerName = "Effect";
		text.GetComponent<MeshRenderer>().sortingOrder = 0;
		text.GetComponent<TextMesh> ().text = "-" + damage.ToString ();
		*/
		health -= damage;
		if (0 >= health) {
			actionState = ActionState.Dead;
		}
	}
}
