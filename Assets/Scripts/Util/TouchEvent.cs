using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class TouchEvent : MonoBehaviour {
    public string eventName;
    public GameObject holder;
	public delegate void OnTouchDownDelegate(Vector3 position);
    public delegate void OnTouchUpDelegate();
    public delegate void OnTouchDragDelegate(Vector3 delta);
    public OnTouchDownDelegate onTouchDown;
    public OnTouchUpDelegate onTouchUp;
    public OnTouchDragDelegate onTouchDrag;
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("TouchEvent");
    }
}
