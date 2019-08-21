using UnityEngine;
using System.Collections;

public class UnitAttack_LightningField : UnitAttack {
	public int lightningCount;
	public int bonusLightningCount;
	public Vector3 initPosition;

	public TouchEvent unitTouchEvent;
	public TouchEvent touchEvent;
	public Vector2 bound;
	void Start()
	{
		touchEvent.gameObject.SetActive (false);
	}

	public override void Init()
	{
		touchEvent.onTouchDown = null;
		touchEvent.onTouchDrag = null;
		touchEvent.onTouchUp = null;
		touchEvent.gameObject.SetActive (false);
	}

	public override void Attack()
	{
        Time.timeScale = 0.1f;

		transform.position = initPosition;
		touchEvent.gameObject.SetActive (true);
		touchEvent.onTouchDown += OnTouchDown;
		unitTouchEvent.gameObject.SetActive(false);
		bonusLightningCount = lightningCount;
    }

	IEnumerator Lightning()
	{
		for (int i = 0; i < lightningCount; i++)
		{
			float interval = Random.Range(0.1f, 0.2f);
			yield return new WaitForSeconds(interval);

			SpawnLightning ();
		}
		touchEvent.onTouchDown -= AddLightning;
		touchEvent.gameObject.SetActive (false);
		unitTouchEvent.gameObject.SetActive (true);
	}

	public void OnTouchDown(Vector3 position)
	{
		transform.position = new Vector3 (position.x, transform.position.y, transform.position.z);
		touchEvent.onTouchDrag += OnTouchDrag;
		touchEvent.onTouchUp += OnTouchUp;
		touchEvent.onTouchDown -= OnTouchDown;
	}
	public void OnTouchDrag(Vector3 delta)
	{
		transform.position = new Vector3(transform.position.x + delta.x, transform.position.y, transform.position.z);
	}
	public void OnTouchUp(Vector3 position)
	{
		Time.timeScale = GameManager.Instance.timeScale;
		StartCoroutine(Lightning());
		touchEvent.onTouchDrag -= OnTouchDrag;
		touchEvent.onTouchUp -= OnTouchUp;
		touchEvent.onTouchDown += AddLightning;
	}
	private void AddLightning(Vector3 position)
	{
		if (0 >= bonusLightningCount) {
			return;
		}
		bonusLightningCount -= 1;
		SpawnLightning ();
	}

	private void SpawnLightning()
	{
		Vector3 position = new Vector3(Random.Range(transform.position.x - bound.x/2, transform.position.x + bound.x/2), Random.Range(transform.position.y - bound.y/2, transform.position.y + bound.y/2), 0.0f);
		Hit (position);
	}
}
