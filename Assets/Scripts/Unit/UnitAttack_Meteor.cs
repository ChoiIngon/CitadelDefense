using UnityEngine;
using System.Collections;

public class UnitAttack_Meteor : UnitAttack {
    public Missile missilePrefab;
    public int missileCount;
	public int bounsMissileCount;
    public Vector3 initPosition;
    public TouchEvent unitTouchEvent;
    public TouchEvent touchEvent;

	public void Start()
	{
		touchEvent.gameObject.SetActive (false);
	}

    public override void Attack()
	{
		Time.timeScale = 0.1f;
		transform.position = initPosition;
		touchEvent.gameObject.SetActive (true);
		touchEvent.onTouchDown += OnTouchDown;
		unitTouchEvent.gameObject.SetActive(false);
		bounsMissileCount = missileCount;
    }

    IEnumerator Meteor()
    {
        Vector3 curPosition = transform.position;
        for (int i = 0; i < missileCount; i++)
        {
            float interval = Random.Range(0.1f, 0.2f);
            yield return new WaitForSeconds(interval);

            Vector3 endPosition = new Vector3(Random.Range(curPosition.x - 2.0f, curPosition.x + 2.0f), Random.Range(curPosition.y + 2.0f, curPosition.y - 2.0f), 0.0f);
            Vector3 startPosition = new Vector3(endPosition.x - 4.0f, endPosition.y + 7.0f, endPosition.z);
            Missile missile = Object.Instantiate<Missile>(missilePrefab);
            missile.Init(startPosition, endPosition, data.power);
        }
		touchEvent.onTouchDown -= AddMeteor;
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
    public void OnTouchUp()
    {
        if(false == gameObject.activeSelf)
        {
            return;
        }
		Time.timeScale = GameManager.Instance.timeScale;
        StartCoroutine(Meteor());
        touchEvent.onTouchDrag -= OnTouchDrag;
		touchEvent.onTouchUp -= OnTouchUp;
		touchEvent.onTouchDown += AddMeteor;
    }
	private void AddMeteor(Vector3 position)
	{
		if (0 >= bounsMissileCount) {
			return;
		}
		bounsMissileCount -= 1;
		Vector3 curPosition = transform.position;
		Vector3 endPosition = new Vector3(Random.Range(curPosition.x - 2.0f, curPosition.x + 2.0f), Random.Range(curPosition.y + 2.0f, curPosition.y - 2.0f), 0.0f);
		Vector3 startPosition = new Vector3(endPosition.x - 4.0f, endPosition.y + 7.0f, endPosition.z);
		Missile missile = Object.Instantiate<Missile>(missilePrefab);
		missile.Init(startPosition, endPosition, data.power);
	}
}
