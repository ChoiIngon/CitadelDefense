using UnityEngine;
using System.Collections;

public class UnitAttack_Meteor : UnitAttack {
    public Missile missilePrefab;
    public int missileCount;

    public Vector3 initPosition;
    private Vector3 targetPosition;
    public TouchEvent unitTouchEvent;
    public TouchEvent touchEvent;
    void OnEnable()
    {
        transform.position = initPosition;
        touchEvent.gameObject.SetActive(true);
    }

    void Start()
    {
        touchEvent.onTouchUp += OnTouchUp;
        touchEvent.onTouchDrag += OnTouchDrag;
    }

    public override void Attack()
	{
    }

    IEnumerator Meteor()
    {
        Vector3 curPosition = transform.position;
        Debug.Log("cur position:" + curPosition);
        for (int i = 0; i < missileCount; i++)
        {
            float interval = Random.Range(0.1f, 0.2f);
            yield return new WaitForSeconds(interval);

            Vector3 endPosition = new Vector3(Random.Range(curPosition.x - 2.0f, curPosition.x + 2.0f), Random.Range(curPosition.y + 2.0f, curPosition.y - 2.0f), 0.0f);
            Debug.Log("end position:" + endPosition);
            Vector3 startPosition = new Vector3(endPosition.x - 4.0f, endPosition.y + 7.0f, endPosition.z);
            Missile missile = Object.Instantiate<Missile>(missilePrefab);
            missile.Init(startPosition, endPosition, data.power);
        }
        gameObject.SetActive(false);
        unitTouchEvent.gameObject.SetActive(true);
        touchEvent.onTouchDrag += OnTouchDrag;
    }

    public void OnTouchUp()
    {
        if(false == gameObject.activeSelf)
        {
            return;
        }
        Time.timeScale = 1.0f;
        StartCoroutine(Meteor());
        touchEvent.onTouchDrag -= OnTouchDrag;
    }

    public void OnTouchDrag(Vector3 delta)
    {
        transform.position = new Vector3(transform.position.x + delta.x, transform.position.y, transform.position.z);
    }
}
