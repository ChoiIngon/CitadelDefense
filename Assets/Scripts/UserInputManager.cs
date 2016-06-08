using UnityEngine;
using System.Collections;

public class UserInputManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 20.0f, 1 << LayerMask.NameToLayer("TouchEvent"));
			if (null != hit.collider)
			{
                TouchEvent touchEvent = hit.collider.gameObject.GetComponent<TouchEvent>();
                touchEvent.onEvent();
			}
		}
	}
}
