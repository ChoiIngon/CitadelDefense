using UnityEngine;
using System.Collections;

public class UnitAttack_LightningField : UnitAttack {
	public GameObject lightning;
	public int lightningCount;
	public Vector3 initPosition;

	public TouchEvent unitTouchEvent;
	public TouchEvent touchEvent;
	void OnEnable()
	{
		transform.position = initPosition;
		touchEvent.gameObject.SetActive(true);
		touchEvent.onTouchUp += OnTouchUp;
		touchEvent.onTouchDrag += OnTouchDrag;
	}

	public override void Attack()
	{
	}

	IEnumerator Lightning()
	{
		for (int i = 0; i < lightningCount; i++)
		{
			float interval = Random.Range(0.1f, 0.2f);
			yield return new WaitForSeconds(interval);

			Vector3 position = new Vector3(Random.Range(transform.position.x - 2.0f, transform.position.x + 2.0f), Random.Range(transform.position.y + 1.5f, transform.position.y - 1.5f), 0.0f);
			GameObject obj = GameObject.Instantiate<GameObject>(lightning);
			obj.transform.position = position;
			UnitColliderAttack attack = obj.GetComponent<UnitColliderAttack> ();
			attack.power = data.power;
			UnitAnimation anim = obj.GetComponent<UnitAnimation> ();
			anim.onComplete += (Animator animator) => {
				DestroyImmediate(animator.gameObject);
			};
		}
		gameObject.SetActive(false);
		if (GameManager.GameState.Play == GameManager.Instance.state) {
			unitTouchEvent.gameObject.SetActive (true);
		}
	}

	public void OnTouchUp()
	{
		Time.timeScale = 1.0f;
		StartCoroutine(Lightning());
		touchEvent.onTouchUp -= OnTouchUp;
		touchEvent.onTouchDrag -= OnTouchDrag;
	}

	public void OnTouchDrag(Vector3 delta)
	{
		transform.position = new Vector3(transform.position.x + delta.x, transform.position.y, transform.position.z);
	}
}
