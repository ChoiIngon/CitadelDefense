using UnityEngine;
using System.Collections;

public class Buff_Poison : MonoBehaviour {
	public float damage;
	public float interval;
	public float time;
	private Unit target;
	private float deltaTime;
	// Use this for initialization
	void Start () {
		deltaTime = 0.0f;
		target = GetComponentInParent<Unit> ();
		if (null == target) {
			return;
		}
		if (null == target.unitAnimation) {
			return;
		}
		StartCoroutine (Damage ());
		target.unitAnimation.spriteRenderer.color = Color.green;
	}

	IEnumerator Damage()
	{
		while (deltaTime < time) {
			target.Damage ((int)damage);
			yield return new WaitForSeconds (interval);
		}
		target.unitAnimation.spriteRenderer.color = Color.white;
		target = null;
	}
	// Update is called once per frame
	void Update () {
		if (null == target) {
			Destroy (gameObject);
		}
		deltaTime += Time.deltaTime;
	}
}
