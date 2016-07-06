using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserInputManager : MonoBehaviour {
    private Dictionary<TouchEvent, TouchEvent> touchEvents = new Dictionary<TouchEvent, TouchEvent>();
    Vector3 lastMousePosition;
	// Use this for initialization
	void Start () {
        lastMousePosition = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 20.0f, 1 << LayerMask.NameToLayer("TouchEvent"));
			if (null != hit.collider)
			{
                TouchEvent touchEvent = hit.collider.gameObject.GetComponent<TouchEvent>();
                if (null != touchEvent.onTouchDown)
                {
                    touchEvent.onTouchDown();
                }
                touchEvents.Add(touchEvent, touchEvent);
			}
		}
        foreach(var v in touchEvents)
        {
            TouchEvent touchEvent = v.Value;
            if(null != touchEvent.onTouchDrag)
            {
                touchEvent.onTouchDrag(Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition);
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            foreach (var v in touchEvents)
            {
                TouchEvent touchEvent = v.Value;
                if (null != touchEvent.onTouchUp)
                {
                    touchEvent.onTouchUp();
                }
            }
            touchEvents.Clear();
        }
        
        lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
