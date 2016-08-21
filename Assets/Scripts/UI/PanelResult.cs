using UnityEngine;
using System.Collections;

public class PanelResult : MonoBehaviour {
	public Sprite win;
	public Sprite lose;
	// Use this for initialization
	public Vector3 start;
	public Vector3 end;
	public float time;

	private Vector3 direction;
	private float interpolate;
	private RectTransform rectTransform;
	void Start () {
		//iTween.MoveTo (gameObject, Camera.main.ScreenToWorldPoint (Vector3.zero), 1.0f);
		rectTransform = GetComponent<RectTransform>();
		direction = Vector3.down;
		interpolate = 0.0f;
		rectTransform.localPosition = start;
	}

	void OnEnable() {
		
	}

	// Update is called once per frame
	void Update () {
		if (1.0f > interpolate) {
			interpolate += Time.deltaTime;
		} else {
			interpolate -= Time.deltaTime;
		}
		rectTransform.localPosition = Vector3.Lerp (start, end, interpolate);
		if (interpolate < 0.0f) {
			gameObject.SetActive (false);
		}
	}
}
