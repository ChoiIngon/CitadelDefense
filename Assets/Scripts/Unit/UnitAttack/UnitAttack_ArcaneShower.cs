using UnityEngine;
using System.Collections;

public class UnitAttack_ArcaneShower : UnitAttack {
	public Missile missilePrefab;
	public int missileCount;
	public int bonusCount;
	public Vector3 initPosition;

	public TouchEvent unitTouchEvent;
	public TouchEvent touchEvent;
	public Vector3 boundary;
	// Use this for initialization
	void Start () {
		touchEvent.gameObject.SetActive (false);
	}

	public override void Attack()
	{
		Time.timeScale = 0.1f;
		transform.position = initPosition;
		touchEvent.gameObject.SetActive (true);
		touchEvent.onTouchDown += OnTouchDown;
		unitTouchEvent.gameObject.SetActive(false);
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

	public void OnTouchUp()
	{
		Time.timeScale = GameManager.Instance.timeScale;
		StartCoroutine(Fire());
		touchEvent.onTouchDrag -= OnTouchDrag;
		touchEvent.onTouchUp -= OnTouchUp;
		touchEvent.onTouchDown += OnBonusHit;
	}

	private IEnumerator Fire()
	{
		for (int i = 0; i < missileCount; i++)
		{
			float interval = Random.Range(0.01f, 0.05f);
			yield return new WaitForSeconds(interval);

			Spawn ();
		}
		touchEvent.onTouchDown -= OnBonusHit;
		touchEvent.gameObject.SetActive (false);
		unitTouchEvent.gameObject.SetActive (true);
	}

	private void OnBonusHit(Vector3 position)
	{
		if (0 >= bonusCount) {
			return;
		}
		bonusCount -= 1;
		Spawn ();
		Spawn ();
		Spawn ();
	}
		
	private void Spawn()
	{
		Vector3 start = self.transform.position;
		Vector3 end = new Vector3(
			Random.Range(transform.position.x - boundary.x/2, transform.position.x + boundary.x/2), 
			Random.Range(transform.position.y - boundary.y/2, transform.position.y + boundary.y/2), 
			0.0f
		);

		Missile missile = Object.Instantiate<Missile>(missilePrefab);
		missile.Init(start, end, this);
	}
}
