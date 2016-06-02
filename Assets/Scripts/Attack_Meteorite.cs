using UnityEngine;
using System.Collections;

public class Attack_Meteorite : MonoBehaviour {
	public Rect rect;
	public int count;
	public GameObject meteorite;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 target = new Vector3(Random.Range (0.0f, rect.width), Random.Range (0.0f, rect.height), 0.0f);
		Vector3 start = target;
		start.x -= 1.0f;

		GameObject.Instantiate (meteorite, start, Quaternion.identity);
	}
}
