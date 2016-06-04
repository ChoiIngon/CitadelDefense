using UnityEngine;
using System.Collections;

public class UserInputManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("Pressed left click, casting ray.");

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 20.0f, 1 << LayerMask.NameToLayer("Unit"));
			if (null != hit.collider)
			{
				Debug.DrawLine(ray.origin, hit.point);
				Debug.Log("Hit object: " + hit.collider.gameObject.name);
			}
		}
	}
}
