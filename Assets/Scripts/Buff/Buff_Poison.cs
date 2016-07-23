using UnityEngine;
using System.Collections;

public class Buff_Poison : Buff {
	public float damage;
	public float interval;
	public float time;
	private float deltaTime;
	// Use this for initialization
	public override void Start () {
        base.Start();
		deltaTime = 0.0f;
		if (null == unit) {
			return;
		}
		if (null == unit.unitAnimation) {
			return;
		}
		StartCoroutine (Damage ());
        unit.unitAnimation.spriteRenderer.color = Color.green;
	}

	IEnumerator Damage()
	{
		while (deltaTime < time) {
            unit.Damage ((int)damage);
			yield return new WaitForSeconds (interval);
		}
        unit.unitAnimation.spriteRenderer.color = Color.white;
        unit = null;
	}
	// Update is called once per frame
	void Update () {
		if (null == unit) {
			Destroy (gameObject);
		}
		deltaTime += Time.deltaTime;
	}
}
